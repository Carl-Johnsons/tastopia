using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeService.API.DTOs;
using RecipeService.Application.Comments;
using RecipeService.Application.Recipes;
using RecipeService.Application.Tags;
using System.IdentityModel.Tokens.Jwt;

namespace RecipeService.API.Controllers;

[Route("api/recipe")]
[ApiController]
[Authorize]
public class RecipeController : BaseApiController
{
    private readonly IMapper _mapper;
    public RecipeController(ISender sender, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(sender, httpContextAccessor)
    {
        _mapper = mapper;
    }

    [HttpPost("create-recipe")]
    public async Task<IActionResult> CreateRecipe([FromForm] CreateRecipeDTO createRecipeDTO)
    {
        //var listStep = new List<Application.Recipes.StepDTO>();
        //foreach (var step in createRecipeDTO.Steps) {
        //    listStep.Add(new Application.Recipes.StepDTO
        //    {
        //        Content = step.Content,
        //        Images = step.Images,
        //        OrdinalNumber = step.OrdinalNumber,
        //    });
        //}


        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var command = _mapper.Map<CreateRecipeCommand>(createRecipeDTO);
        command.AuthorId = Guid.Parse(subjectId!);
        var result = await _sender.Send(command);
        //var result = await _sender.Send(new CreateRecipeCommand
        //{
        //   AuthorId = Guid.Parse(subjectId!),
        //   Title = createRecipeDTO.Title,
        //   CookTime = createRecipeDTO.CookTime,
        //   Description = createRecipeDTO.Description,
        //   Ingredients = createRecipeDTO.Ingredients,
        //   RecipeImage = createRecipeDTO.RecipeImage,
        //   Serves = createRecipeDTO.Serves,
        //   Steps = listStep,

        //});
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpPost("get-recipe-feed")]
    public async Task<IActionResult> GetRecipeFeed([FromBody] GetRecipeFeedsDTO getRecipeFeedsDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        await Console.Out.WriteLineAsync("subjectId:"+ string.IsNullOrEmpty(subjectId));

        if (string.IsNullOrEmpty(subjectId))
        {
            var resultGuest = await _sender.Send(new GetRecipeFeedsForGuestCommand
            {
                TagValues = getRecipeFeedsDTO.TagValues
            });
            resultGuest.ThrowIfFailure();
            return Ok(resultGuest.Value);
        }
     

        var result = await _sender.Send(new GetRecipeFeedsCommand
        {
            Skip = getRecipeFeedsDTO.Skip,
            TagValues = getRecipeFeedsDTO.TagValues,
            AccountId = Guid.Parse(subjectId!),
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("search-recipe")]
    public async Task<IActionResult> SearchRecipe([FromBody] SearchRecipesDTO searchRecipesDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var result = await _sender.Send(new SearchRecipesCommand
        {
            Skip = searchRecipesDTO.Skip,
            TagValues = searchRecipesDTO.TagValues,
            Keyword = searchRecipesDTO.Keyword,
            AccountId = Guid.Parse(subjectId!),
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

    [HttpPost("vote-recipe")]
    public async Task<IActionResult> VoteRecipe([FromBody] VoteRecipeDTO voteRecipeDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new VoteRecipeCommand
        {
            AccountId = Guid.Parse(subjectId!),
            IsUpvote = voteRecipeDTO.IsUpvote,
            RecipeId = voteRecipeDTO.RecipeId
        });
        result.ThrowIfFailure();
        return Ok();
    }

    [HttpPost("get-recipe-comments")]
    public async Task<IActionResult> GetRecipeComments([FromBody] GetRecipeCommentsDTO getRecipeCommentsDTO)
    {
        var result = await _sender.Send(new GetRecipeCommentsCommand
        {
            RecipeId = getRecipeCommentsDTO.RecipeId,
            Skip = getRecipeCommentsDTO.Skip
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }


    [HttpPost("comment-recipe")]
    public async Task<IActionResult> CommentRecipe([FromBody] CommentRecipeDTO commentRecipeDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new CommentRecipeCommand
        {
            AccountId = Guid.Parse(subjectId!),
            RecipeId = commentRecipeDTO.RecipeId,
            Content = commentRecipeDTO.Content,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }
}
