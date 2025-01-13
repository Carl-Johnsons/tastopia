
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;

namespace RecipeService.Application.Recipes.Commands;

public class RequestAddTagsCommand : IRequest<Result>
{
    public List<string>? Values { get; set; }
    public Guid? RecipeId { get; set; }
}

public class RequestAddTagsCommandHandler : IRequestHandler<RequestAddTagsCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public RequestAddTagsCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RequestAddTagsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var values = request.Values;
            var recipeId = request.RecipeId;
            if (recipeId == null || values == null || values.Count == 0)
            {
                return Result.Failure(TagError.AddTagFail);
            }

            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == recipeId);

            if (recipe == null)
            {
                return Result.Failure(TagError.AddTagFail);
            }

            var requestedTags = new List<Tag>();
            var recipeRequestedTags = new List<RecipeTag>();

            foreach (var value in values)
            {
                var tag = new Tag
                {
                    Id = Guid.NewGuid(),
                    Value = value,
                    Category = TagCategory.All,
                    Code = Guid.NewGuid().ToString(),
                    ImageUrl = "",
                    Status = TagStatus.Requested,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                requestedTags.Add(tag);
                recipeRequestedTags.Add(new RecipeTag
                {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe.Id,
                    TagId = tag.Id
                });
            }
            _context.Tags.AddRange(requestedTags);
            _context.RecipeTags.AddRange(recipeRequestedTags);
            await _unitOfWork.SaveChangeAsync();
            return Result.Success();

        }
        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(ex, Formatting.Indented)+ex.StackTrace);
            return Result.Failure(TagError.AddTagFail);

        }
    }
}


