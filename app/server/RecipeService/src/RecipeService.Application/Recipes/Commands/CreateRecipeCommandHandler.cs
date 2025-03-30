using Contract.Constants;
using Contract.DTOs.UploadFileDTO;
using Contract.Event.NotificationEvent;
using Contract.Event.RecipeEvent;
using Contract.Event.UploadEvent;
using Contract.Event.UserEvent;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Newtonsoft.Json;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using System.ComponentModel.DataAnnotations;
using UploadFileProto;
using UserProto;
namespace RecipeService.Application.Recipes.Commands;
public record CreateRecipeCommand : IRequest<Result<Recipe?>>
{
    public Guid AuthorId { get; set; }

    public IFormFile RecipeImage { get; init; } = null!;

    public string Title { get; init; } = null!;

    public string Description { get; init; } = null!;

    public int? Serves { get; init; }

    public string? CookTime { get; init; }

    public List<string> Ingredients { get; init; } = null!;

    public List<StepDTO> Steps { get; init; } = null!;

    public List<string>? TagValues { get; set; }
}
public class StepDTO
{
    [Required]
    public int OrdinalNumber { get; init; }

    [Required]
    [MaxLength(500)]
    public string Content { get; init; } = null!;
    public List<IFormFile>? Images { get; init; } = null!;
}
public class CreateRecipeCommandHandler : IRequestHandler<CreateRecipeCommand, Result<Recipe?>>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServiceBus _serviceBus;
    private readonly GrpcUploadFile.GrpcUploadFileClient _grpcUploadFileClient;
    private readonly GrpcUser.GrpcUserClient _grpcUserClient;
    private readonly ILogger<CreateRecipeCommandHandler> _logger;

    public CreateRecipeCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, IServiceBus serviceBus, GrpcUploadFile.GrpcUploadFileClient grpcUploadFileClient, GrpcUser.GrpcUserClient grpcUserClient, ILogger<CreateRecipeCommandHandler> logger)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _serviceBus = serviceBus;
        _grpcUploadFileClient = grpcUploadFileClient;
        _grpcUserClient = grpcUserClient;
        _logger = logger;
    }

    public async Task<Result<Recipe?>> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    {
        List<string>? rollbackUrls = null;
        try
        {
            var steps = request.Steps;
            var imageIndex = GetImageIndexMap(steps);

            var response = await _grpcUploadFileClient.UploadMultipleImageAsync(new GrpcUploadMultipleImageRequest
            {
                FileStreams = { await GetGrpcFileStreamDTOsAsync(request.RecipeImage, steps) },
            }, cancellationToken: cancellationToken);

            if (response == null || response.Files.Count != imageIndex.Count)
            {
                return Result<Recipe?>.Failure(RecipeError.AddRecipeFail);
            }

            rollbackUrls = response.Files.Select(f => f.Url).ToList();

            var recipe = new Recipe();

            recipe.Id = Guid.NewGuid();
            recipe.AuthorId = request.AuthorId;
            recipe.Serves = request.Serves;
            recipe.CookTime = request.CookTime;
            recipe.Title = request.Title;
            recipe.Ingredients = request.Ingredients;
            recipe.Description = request.Description;
            recipe.CreatedAt = DateTime.Now;
            recipe.ImageUrl = response.Files[imageIndex["RecipeImage"]].Url;

            var listSteps = new List<Step>();
            foreach (var step in steps)
            {
                var s = new Step();
                s.Id = Guid.NewGuid();
                s.OrdinalNumber = step.OrdinalNumber;
                s.Content = step.Content;
                s.CreatedAt = DateTime.Now;

                if (step.Images != null && step.Images.Any())
                {
                    var listUrl = new List<string>();
                    for (int i = 0; i < step.Images.Count; i++)
                    {
                        listUrl.Add(response.Files[imageIndex[$"Step{step.OrdinalNumber}|{i}"]].Url);
                    }

                    s.AttachedImageUrls = listUrl;
                }
                listSteps.Add(s);
            }

            recipe.Steps = listSteps;

            _context.Recipes.Add(recipe);
            await _unitOfWork.SaveChangeAsync(cancellationToken);

            await _serviceBus.Publish(new ValidateRecipeEvent
            {
                RecipeId = recipe.Id,
                TagValues = request.TagValues ?? []
            });

            var recipientId = await _grpcUserClient.GetUserFollowerAsync(new GrpcAccountIdRequest
            {
                AccountId = request.AuthorId.ToString()
            });

            var ids = recipientId.AccountIds.Select(Guid.Parse).ToList();

            if(ids != null && ids.Count != 0) {
                await _serviceBus.Publish(new NotifyUserEvent
                {
                    PrimaryActors = [
                        new ActorDTO
                        {
                            ActorId = request.AuthorId,
                            Type = EntityType.USER
                        }],
                    SecondaryActors = [],
                    TemplateCode = NotificationTemplateCode.USER_CREATE_RECIPE,
                    Channels = [NOTIFICATION_CHANNEL.DEFAULT],
                    JsonData = JsonConvert.SerializeObject(new
                    {
                        redirectUri = $"{CLIENT_URI.MOBILE.COMMUNITY}/{recipe.Id}"
                    }),
                    ImageUrl = recipe.ImageUrl,
                    RecipientIds = ids
                });
            }
            await _serviceBus.Publish(new UpdateUserTotalRecipeEvent
            {
                AccountId = request.AuthorId,
                Delta = 1
            });

            return Result<Recipe?>.Success(recipe);
        }
        catch (Exception ex)
        {
            _logger.LogError(JsonConvert.SerializeObject(ex, Formatting.Indented));
            await RollBackImageGrpc(rollbackUrls);
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

    private Dictionary<string, int> GetImageIndexMap(List<StepDTO> steps)
    {
        Dictionary<string, int> map = new Dictionary<string, int>();
        map.Add("RecipeImage", 0);
        int size = 1;

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
    private async Task<RepeatedField<GrpcFileStreamDTO>> GetGrpcFileStreamDTOsAsync(IFormFile recipeImage, List<StepDTO> steps)
    {
        var tasks = new List<Task<(int Index, GrpcFileStreamDTO StreamDto)>>();
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

        int index = 1;
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

