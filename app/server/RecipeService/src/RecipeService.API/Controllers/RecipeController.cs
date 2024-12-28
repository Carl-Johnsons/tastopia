using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeService.API.DTOs;
using RecipeService.Application.Recipes;
using RecipeService.Application.Tags;

namespace RecipeService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class RecipeController : BaseApiController
{
    public RecipeController(ISender sender, IHttpContextAccessor httpContextAccessor) : base(sender, httpContextAccessor)
    {
    }

    [HttpPost("create-recipe")]
    public async Task<IActionResult> UpdateImage([FromForm] CreateRecipeDTO createRecipeDTO)
    {
        var listStep = new List<Application.Recipes.StepDTO>();
        foreach (var step in createRecipeDTO.Steps) {
            await Console.Out.WriteLineAsync("******************************"+step.OrdinalNumber);
            listStep.Add(new Application.Recipes.StepDTO
            {
                Content = step.Content,
                Images = step.Images,
                OrdinalNumber = step.OrdinalNumber,
            });
        }

        var result = await _sender.Send(new CreateRecipeCommand
        {
           AuthorId = Guid.Parse("61c61ac7-291e-4075-9689-666ef05547ed"),
           Title = createRecipeDTO.Title,
           CookTime = createRecipeDTO.CookTime,
           Description = createRecipeDTO.Description,
           Ingredients = createRecipeDTO.Ingredients,
           RecipeImage = createRecipeDTO.RecipeImage,
           Serves = createRecipeDTO.Serves,
           Steps = listStep,

        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }


    [HttpPost("get-recipe-feed")]
    public async Task<IActionResult> GetRecipeFeed([FromBody] GetRecipeFeedsDTO getRecipeFeedsDTO)
    {
        var result = await _sender.Send(new GetRecipeFeedsCommand
        {
            Skip = getRecipeFeedsDTO.Skip,
            TagValues = getRecipeFeedsDTO.TagValues,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("search-recipe")]
    public async Task<IActionResult> SearchRecipe([FromBody] SearchRecipesDTO searchRecipesDTO)
    {
        var result = await _sender.Send(new SearchRecipesCommand
        {
            Skip = searchRecipesDTO.Skip,
            TagValues = searchRecipesDTO.TagValues,
            Keyword = searchRecipesDTO.Keyword,
            UserId = Guid.Parse("61c61ac7-291e-4075-9689-666ef05547ed"),
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("get-tag")]
    public async Task<IActionResult> GetTag([FromBody] GetTagsDTO getTagsDTO)
    {
        var result = await _sender.Send(new GetTagsCommand
        {
            Skip = getTagsDTO.Skip,
            TagCodes = getTagsDTO.TagCodes,
            Keyword = getTagsDTO.Keyword,
            Category = getTagsDTO.Category
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("get-recipe-details")]
    public async Task<IActionResult> GetRecipeDetails([FromBody] GetRecipeDetailDTO getRecipeDetailDTO)
    {
        var result = await _sender.Send(new GetRecipeDetailCommand
        {
            RecipeId = getRecipeDetailDTO.RecipeId,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

}
