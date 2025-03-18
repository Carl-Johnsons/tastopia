using Contract.DTOs.UploadFileDTO;
using Contract.Event.RecipeEvent;
using Contract.Event.UploadEvent;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Newtonsoft.Json;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using System.ComponentModel.DataAnnotations;
using UploadFileProto;


namespace RecipeService.Application.Recipes.Commands;

public record UpdateRecipeCommand : IRequest<Result<Recipe?>>
{
    public Guid Id { get; init; }

    public Guid AuthorId { get; set; }

    public IFormFile? RecipeImage { get; init; }

    public string Title { get; init; } = null!;

    public string Description { get; init; } = null!;

    public int? Serves { get; init; }

    public string? CookTime { get; init; }

    public List<string> Ingredients { get; init; } = null!;

    public List<UpdateStepDTO> Steps { get; init; } = null!;

    public List<string>? TagValues { get; set; }
}

public class UpdateStepDTO
{
    public Guid StepId { get; init; }

    [Required]
    public int OrdinalNumber { get; init; }

    [Required]
    [MaxLength(500)]
    public string Content { get; init; } = null!;
    public List<IFormFile>? Images { get; init; } = null!;
    public List<string>? DeleteUrls { get; init; } = null!;
}

public class UpdateRecipeCommandHandler : IRequestHandler<UpdateRecipeCommand, Result<Recipe?>>
{
    private readonly ILogger<UpdateRecipeCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceBus _serviceBus;
    private readonly GrpcUploadFile.GrpcUploadFileClient _grpcUploadFileClient;

    public UpdateRecipeCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, IServiceBus serviceBus, GrpcUploadFile.GrpcUploadFileClient grpcUploadFileClient, ILogger<UpdateRecipeCommandHandler> logger)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _serviceBus = serviceBus;
        _grpcUploadFileClient = grpcUploadFileClient;
        _logger = logger;
    }

    public async Task<Result<Recipe?>> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        List<string>? rollbaclUrls = null;
        try
        {
            var recipeId = request.Id;
            var authorId = request.AuthorId;
            var steps = request.Steps;

            if(authorId == Guid.Empty || recipeId == Guid.Empty || steps == null || steps.Count == 0)
            {
                return Result<Recipe?>.Failure(RecipeError.NullParameter);
            }
            var recipe = await _context.Recipes.Where(r => r.Id == recipeId && r.IsActive).SingleOrDefaultAsync();

            if(recipe == null)
            {
                return Result<Recipe?>.Failure(RecipeError.NotFound, "Not found recipe");
            }

            if (!recipe.AuthorId.Equals(authorId)) { 
                return Result<Recipe?>.Failure(RecipeError.PermissionDeny);

            }

            var imageIndex = GetImageIndexMap(request.RecipeImage, steps);
            var deleteUrls = GetDeleteUrls(recipe, request.RecipeImage, steps);

            var images = await GetGrpcFileStreamDTOsAsync(request.RecipeImage, steps);
            var files = new RepeatedField<GrpcFileDTO>();
            if(images != null && images.Count != 0)
            {
                var response = await _grpcUploadFileClient.UploadMultipleImageAsync(new GrpcUploadMultipleImageRequest
                {
                    FileStreams = { images },
                }, cancellationToken: cancellationToken);

                if (response == null || response.Files.Count != imageIndex.Count)
                {
                    return Result<Recipe?>.Failure(RecipeError.AddRecipeFail);
                }
                files = response.Files;
                rollbaclUrls = response.Files.Select(f => f.Url).ToList();
            }
            if(deleteUrls != null && deleteUrls.Count != 0)
            {
                await _serviceBus.Publish(new DeleteMultipleFileEvent
                {
                    DeleteUrl = deleteUrls
                });
            }
            recipe.Serves = request.Serves;
            recipe.CookTime = request.CookTime;
            recipe.Title = request.Title;
            recipe.Ingredients = request.Ingredients;
            recipe.Description = request.Description;
            recipe.UpdatedAt = DateTime.Now;

            if (request.RecipeImage != null) { 
                recipe.ImageUrl = files[imageIndex["RecipeImage"]].Url;
            }
            var listSteps = new List<Step>();
            foreach (var step in steps)
            {
                var s = recipe.Steps.Where(s => s.Id == step.StepId).SingleOrDefault();
                if (s == null) {
                    s = new Step();
                    s.Id = step.StepId != Guid.Empty ? step.StepId : Guid.NewGuid();
                    s.AttachedImageUrls = [];
                    s.CreatedAt = DateTime.Now;
                }
                s.OrdinalNumber = step.OrdinalNumber;
                s.Content = step.Content;
                s.UpdatedAt = DateTime.Now;

                if (step.Images != null && step.Images.Count != 0)
                {
                    var listUrl = new List<string>();
                    for (int i = 0; i < step.Images.Count; i++)
                    {
                        listUrl.Add(files[imageIndex[$"Step{step.OrdinalNumber}|{i}"]].Url);
                    }
                    if(s.AttachedImageUrls == null)
                    {
                        s.AttachedImageUrls = [];
                    }
                    s.AttachedImageUrls.AddRange(listUrl);
                }
                listSteps.Add(s);
            }

            recipe.Steps = listSteps;

            _context.Recipes.Update(recipe);
            await _unitOfWork.SaveChangeAsync(cancellationToken);

            await _serviceBus.Publish(new ValidateRecipeEvent
            {
                RecipeId = recipe.Id,
                TagValues = request.TagValues ?? []
            });

            return Result<Recipe?>.Success(recipe);


        }
        catch (Exception ex)
        {
            _logger.LogError(JsonConvert.SerializeObject(ex, Formatting.Indented));
            await RollBackImageGrpc(rollbaclUrls);
        }

        return Result<Recipe?>.Failure(RecipeError.AddRecipeFail);

    }

    public async Task RollBackImage(List<FileDTO>? files)
    {
        if (files == null) return;
        var listUrls = new List<string>();
        foreach (var file in files)
        {
            listUrls.Add(file.Url);
        }
        await _serviceBus.Publish(new DeleteMultipleFileEvent
        {
            DeleteUrl = listUrls,
        });
    }

    public async Task RollBackImageGrpc(List<string>? urls)
    {
        if (urls == null || urls.Count == 0) return;
        await _serviceBus.Publish(new DeleteMultipleFileEvent
        {
            DeleteUrl = urls
        });
    }

    private Dictionary<string, int> GetImageIndexMap(IFormFile? recipeImage, List<UpdateStepDTO> steps)
    {
        Dictionary<string, int> map = new Dictionary<string, int>();

        int size = 0;
        if(recipeImage != null)
        {
            map.Add("RecipeImage", 0);
            size = 1;
        }
        foreach (var step in steps)
        {
            if (step.Images != null && step.Images.Any())
            {
                for (int i = 0; i < step.Images.Count; i++)
                {
                    map.Add($"Step{step.OrdinalNumber}|{i}", size);
                    size++;
                }
            }
        }
        return map;
    }

    //Remove all step that need to delete and return an deleteUrls
    private List<string> GetDeleteUrls(Recipe recipe, IFormFile? recipeImage, List<UpdateStepDTO> steps)
    {
        var deleteUrls = new List<string>();
        if (recipeImage != null)
        {
            deleteUrls.Add(recipe.ImageUrl);
        }

        var stepIds = steps.Where(s => s.StepId != Guid.Empty).Select(s => s.StepId).ToList();

        foreach(var step in recipe.Steps)
        {
            //if update steps contain recipe step
            if (stepIds != null && stepIds.Count != 0 && stepIds.Contains(step.Id))
            {
                var updateStep = steps.Where(s => s.StepId == step.Id).SingleOrDefault();
                if (updateStep == null)
                {
                    continue;
                }
                if(updateStep.DeleteUrls == null || updateStep.DeleteUrls.Count == 0)
                {
                    Console.WriteLine("bi null roi neeeeeeeeeeeeeeeeeeeeeeeeeee");
                    continue;
                }
                deleteUrls.AddRange(updateStep.DeleteUrls);
                if(step.AttachedImageUrls == null || step.AttachedImageUrls.Count == 0)
                {
                    continue;
                }
                step.AttachedImageUrls.RemoveAll(s => updateStep.DeleteUrls.Contains(s));
                continue;
            }
            if (step.AttachedImageUrls != null && step.AttachedImageUrls.Count != 0) {
                deleteUrls.AddRange(step.AttachedImageUrls);
            }
        }

        if (stepIds == null || stepIds.Count == 0) { 
            recipe.Steps.Clear();
        }
        else { 
            recipe.Steps.RemoveAll(s => !stepIds.Contains(s.Id));
        }
        return deleteUrls;
    }

    private List<FileStreamDTO> GetFileStreamDTOs(IFormFile recipeImage, List<StepDTO> steps)
    {
        var list = new List<FileStreamDTO>();

        list.Add(new FileStreamDTO
        {
            FileName = recipeImage.FileName,
            ContentType = recipeImage.ContentType,
            Stream = new BinaryReader(recipeImage.OpenReadStream()).ReadBytes((int)recipeImage.Length)
        });

        foreach (var step in steps)
        {
            if (step.Images != null && step.Images.Any())
            {
                foreach (var img in step.Images)
                {
                    list.Add(new FileStreamDTO
                    {
                        FileName = img.FileName,
                        ContentType = img.ContentType,
                        Stream = new BinaryReader(img.OpenReadStream()).ReadBytes((int)img.Length)
                    });
                }
            }
        }
        return list;
    }
    private async Task<RepeatedField<GrpcFileStreamDTO>> GetGrpcFileStreamDTOsAsync(IFormFile? recipeImage, List<UpdateStepDTO> steps)
    {
        int index = 0;
        var tasks = new List<Task<(int Index, GrpcFileStreamDTO StreamDto)>>();
        if (recipeImage != null)
        {
            index = 1;
            tasks.Add(Task.Run(async () =>
            {
                var stream = await ByteString.FromStreamAsync(recipeImage.OpenReadStream());
                return (Index: 0, StreamDto: new GrpcFileStreamDTO
                {
                    FileName = recipeImage.FileName,
                    ContentType = recipeImage.ContentType,
                    Stream = stream
                });
            }));
        }
   
        foreach (var step in steps)
        {
            if (step.Images != null && step.Images.Any())
            {
                foreach (var img in step.Images)
                {
                    var currentIndex = index++;
                    tasks.Add(Task.Run(async () =>
                    {
                        var stream = await ByteString.FromStreamAsync(img.OpenReadStream());
                        return (Index: currentIndex, StreamDto: new GrpcFileStreamDTO
                        {
                            FileName = img.FileName,
                            ContentType = img.ContentType,
                            Stream = stream
                        });
                    }));
                }
            }
        }

        var results = await Task.WhenAll(tasks);
        var list = results.OrderBy(result => result.Index).Select(result => result.StreamDto).ToList();
        RepeatedField<GrpcFileStreamDTO> repeatedField = new RepeatedField<GrpcFileStreamDTO>();
        repeatedField.AddRange(list);
        return repeatedField;
    }


}

