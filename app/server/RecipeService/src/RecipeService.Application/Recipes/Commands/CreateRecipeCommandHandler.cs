using Contract.DTOs;
using Contract.Event.UploadEvent;
using Contract.Event.UploadEvent.EventModel;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using Newtonsoft.Json;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;
using System.ComponentModel.DataAnnotations;


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

    public List<string>? TagCodes { get; set; }

    public List<string>? AdditionTagValues { get; set; }
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

    public CreateRecipeCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, IServiceBus serviceBus)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _serviceBus = serviceBus;
    }

    public async Task<Result<Recipe?>> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    {
        List<UploadImageFileEventResponseDTO>? rollBackFiles = null;


        //var id = Guid.Parse("057aa844-742a-4952-8162-dbfbd7e493ac");
        //var recipeColection = _context.GetDatabase().GetCollection<Recipe>(nameof(Recipe)).AsQueryable();
        //var tagsColection = _context.GetDatabase().GetCollection<Domain.Entities.Tag>(nameof(Domain.Entities.Tag)).AsQueryable();

        //var result = recipeColection.SelectMany(r => r.Comments).ToList();

        //await Console.Out.WriteLineAsync("**************************************************");

        //foreach (var r in result)
        //{
        //    await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(r));

        //}



        //return Result<Recipe?>.Failure(RecipeError.AddRecipeFail);

        try
        {
            var steps = request.Steps;
            var imageIndex = GetImageIndexMap(steps);
            var requestClient = _serviceBus.CreateRequestClient<UploadMultipleImageFileEvent>();

            var response = await requestClient.GetResponse<UploadMultipleImageFileEventResponseDTO>(new UploadMultipleImageFileEvent
            {
                FileStreamEvents = GetFileSteamEvent(request.RecipeImage, steps),
            });

            if (response == null || response.Message.Files.Count != imageIndex.Count)
            {
                return Result<Recipe?>.Failure(RecipeError.AddRecipeFail);
            }

            rollBackFiles = response.Message.Files;

            var recipe = new Recipe();

            recipe.AuthorId = request.AuthorId;
            recipe.Serves = request.Serves;
            recipe.CookTime = request.CookTime;
            recipe.Title = request.Title;
            recipe.Ingredients = request.Ingredients;
            recipe.Description = request.Description;
            recipe.CreatedAt = DateTime.Now;
            recipe.ImageUrl = response.Message.Files[imageIndex["RecipeImage"]].Url;

            var listSteps = new List<Step>();
            foreach (var step in steps)
            {
                var s = new Step();
                s.OrdinalNumber = step.OrdinalNumber;
                s.Content = step.Content;
                s.CreatedAt = DateTime.Now;

                if (step.Images != null && step.Images.Any())
                {
                    var listUrl = new List<string>();
                    for (int i = 0; i < step.Images.Count; i++)
                    {
                        listUrl.Add(response.Message.Files[imageIndex[$"Step{step.OrdinalNumber}|{i}"]].Url);
                    }

                    s.AttachedImageUrls = listUrl;
                }
                listSteps.Add(s);
            }

            recipe.Steps = listSteps;

            _context.Recipes.Add(recipe);
            await _unitOfWork.SaveChangeAsync(cancellationToken);

            return Result<Recipe?>.Success(recipe);


        }
        catch (Exception ex)
        {
            await RollBackImage(rollBackFiles);
            await Console.Out.WriteLineAsync(ex.Message);
        }

        return Result<Recipe?>.Failure(RecipeError.AddRecipeFail);

    }

    public async Task RollBackImage(List<UploadImageFileEventResponseDTO>? files)
    {
        if (files == null) return;
        var listUrls = new List<string>();
        foreach (var file in files)
        {
            listUrls.Add(file.Url);
        }
        var requestClient = _serviceBus.CreateRequestClient<DeleteMultipleFileEvent>();
        await requestClient.GetResponse<DeleteMultipleFileEventResponseDTO>(new DeleteMultipleFileEvent
        {
            DeleteUrl = listUrls,
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

    private List<FileStreamEvent> GetFileSteamEvent(IFormFile recipeImage, List<StepDTO> steps)
    {
        var list = new List<FileStreamEvent>();

        list.Add(new FileStreamEvent
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
                    list.Add(new FileStreamEvent
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
}

