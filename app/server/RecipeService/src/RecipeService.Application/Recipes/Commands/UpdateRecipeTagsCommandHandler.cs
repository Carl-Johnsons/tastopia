﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Errors;

namespace RecipeService.Application.Recipes.Commands;

public class UpdateRecipeTagsCommand : IRequest<Result>
{
    public Guid? RecipeId { get; set; }
    public List<string>? TagCodes { get; set; }
}

public class UpdateRecipeTagsCommandHandler : IRequestHandler<UpdateRecipeTagsCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateRecipeTagsCommandHandler> _logger;

    public UpdateRecipeTagsCommandHandler(IApplicationDbContext context, IUnitOfWork unitOfWork, ILogger<UpdateRecipeTagsCommandHandler> logger)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result> Handle(UpdateRecipeTagsCommand request, CancellationToken cancellationToken)
    {
        try {
            var recipeId = request.RecipeId;
            var tagCodes = request.TagCodes;
            if (recipeId == null || tagCodes == null || tagCodes.Count == 0)
            {
                return Result.Failure(RecipeError.UpdateRecipeFail);
            }

            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == recipeId);
            var tags = await _context.Tags.Where(t => tagCodes.Contains(t.Code)).ToListAsync();

            if (recipe == null || tags == null || tags.Count == 0)
            {
                return Result.Failure(RecipeError.UpdateRecipeFail);
            }
            var recipeTags = new List<RecipeTag>();
            _context.RecipeTags.RemoveRange(_context.RecipeTags.Where(rt => rt.RecipeId == recipeId));
            foreach (var tag in tags)
            {
                recipeTags.Add(new RecipeTag
                {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe.Id,
                    TagId = tag.Id
                });
            }
            _context.RecipeTags.AddRange(recipeTags);
            await _unitOfWork.SaveChangeAsync();
            return Result.Success();
        }
        catch (Exception ex) {
            _logger.LogError(JsonConvert.SerializeObject(ex, Formatting.Indented));
            return Result.Failure(RecipeError.UpdateRecipeFail);
        }
    }
}
