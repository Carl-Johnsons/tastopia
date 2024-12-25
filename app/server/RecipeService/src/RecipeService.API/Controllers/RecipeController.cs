using Microsoft.AspNetCore.Mvc;
using RecipeService.API.DTOs;
using RecipeService.Application.Recipes;

namespace RecipeService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
               AuthorId = Guid.Parse("375fca51-5d02-4215-8e7f-7466e12e4a26"),
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


        [HttpGet("get-recipe-feed")]
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

    }

}
