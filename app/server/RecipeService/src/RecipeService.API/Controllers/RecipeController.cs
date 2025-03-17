using AutoMapper;
using Contract.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeService.API.DTOs;
using RecipeService.Application.Comments.Commands;
using RecipeService.Application.Comments.Queries;
using RecipeService.Application.Recipes.Commands;
using RecipeService.Application.Recipes.Queries;
using RecipeService.Application.ReportReasons.Queries;
using RecipeService.Application.Reports.Commands;
using RecipeService.Application.Tags.Queries;
using RecipeService.Application.UserBookmarkRecipes.Commands;
using RecipeService.Application.UserBookmarkRecipes.Queries;
using RecipeService.Application.UserRecipeBins.Queries;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Responses;
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
    [Produces("application/json")]
    [ProducesResponseType(typeof(Recipe), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> CreateRecipe([FromForm] CreateRecipeDTO createRecipeDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var command = _mapper.Map<CreateRecipeCommand>(createRecipeDTO);
        command.AuthorId = Guid.Parse(subjectId!);
        var result = await _sender.Send(command);
        result.ThrowIfFailure();
        return Ok(result.Value);
    }


    [HttpPost("update-recipe")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Recipe), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> UpdateRecipe([FromForm] UpdateRecipeDTO updateRecipeDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var command = _mapper.Map<UpdateRecipeCommand>(updateRecipeDTO);
        command.AuthorId = Guid.Parse(subjectId!);
        var result = await _sender.Send(command);
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("delete-own-recipe")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Recipe), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> DeleteOwnRecipe([FromBody] DeleteOwnRecipeDTO deleteOwnRecipeDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new DeleteOwnRecipeCommand
        {
            AuthorId = Guid.Parse(subjectId!),
            RecipeId = deleteOwnRecipeDTO.RecipeId,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("restore-own-recipe")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Recipe), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> RestoreOwnRecipe([FromBody] RestoreOwnRecipeDTO restoreOwnRecipeDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new RestoreOwnRecipeCommand
        {
            AuthorId = Guid.Parse(subjectId!),
            RecipeId = restoreOwnRecipeDTO.RecipeId,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpPost("get-recipe-feed")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedRecipeFeedsListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetRecipeFeed([FromBody] GetRecipeFeedsDTO getRecipeFeedsDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        if (string.IsNullOrEmpty(subjectId))
        {
            var resultGuest = await _sender.Send(new GetRecipeFeedsForGuestQuery
            {
                TagValues = getRecipeFeedsDTO.TagValues
            });
            resultGuest.ThrowIfFailure();
            return Ok(resultGuest.Value);
        }


        var result = await _sender.Send(new GetRecipeFeedsQuery
        {
            Skip = getRecipeFeedsDTO.Skip,
            TagValues = getRecipeFeedsDTO.TagValues,
            AccountId = Guid.Parse(subjectId!),
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpPost("get-recipe-feed-by-author-id")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedRecipeFeedsListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetRecipeFeedByAccountId([FromBody] GetRecipeFeedsByAuthorIdDTO getRecipeFeedsByAuthorIdDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var accountId = Guid.Empty;

        if (!string.IsNullOrEmpty(subjectId))
        {
            accountId = Guid.Parse(subjectId!);
        }

        var result = await _sender.Send(new GetRecipeFeedsByAuthorIdQuery
        {
            Skip = getRecipeFeedsByAuthorIdDTO.Skip,
            AuthorId = getRecipeFeedsByAuthorIdDTO.AuthorId,
            AccountId = accountId
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("search-recipe")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedSearchRecipeListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> SearchRecipe([FromBody] SearchRecipesDTO searchRecipesDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var result = await _sender.Send(new SearchRecipesQuery
        {
            Skip = searchRecipesDTO.Skip,
            TagCodes = searchRecipesDTO.TagCodes,
            Keyword = searchRecipesDTO.Keyword,
            AccountId = Guid.Parse(subjectId!),
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("get-tag")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedTagListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetTag([FromBody] GetTagsDTO getTagsDTO)
    {
        var result = await _sender.Send(new GetTagsQuery
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
    [Produces("application/json")]
    [ProducesResponseType(typeof(RecipeDetailsResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetRecipeDetails([FromBody] GetRecipeDetailDTO getRecipeDetailDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new GetRecipeDetailQuery
        {
            RecipeId = getRecipeDetailDTO.RecipeId,
            AccountId = Guid.Parse(subjectId!),
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("vote-recipe")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
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
        return Ok(result.Value);
    }

    [HttpPost("get-recipe-comments")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedRecipeCommentListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetRecipeComments([FromBody] GetRecipeCommentsDTO getRecipeCommentsDTO)
    {
        var result = await _sender.Send(new GetRecipeCommentsQuery
        {
            RecipeId = getRecipeCommentsDTO.RecipeId,
            Skip = getRecipeCommentsDTO.Skip
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("get-account-recipe-comments")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedAccountRecipeCommentListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetAccountRecipeComments([FromBody] GetAccountRecipeCommentsDTO getAccountRecipeCommentsDTO)
    {
        var result = await _sender.Send(new GetAccountRecipeCommentsQuery
        {
            AccountId = getAccountRecipeCommentsDTO.AccountId,
            Skip = getAccountRecipeCommentsDTO.Skip
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }


    [HttpPost("comment-recipe")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(RecipeCommentResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
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

    [HttpPost("delete-comment")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Comment), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> DeleteComment([FromBody] DeleteCommentDTO deleteCommentDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new DeleteCommentCommand
        {
            AccountId = Guid.Parse(subjectId!),
            CommentId = deleteCommentDTO.CommentId,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("update-comment")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Comment), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentDTO updateCommentDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new UpdateCommentCommand
        {
            AccountId = Guid.Parse(subjectId!),
            CommentId = updateCommentDTO.CommentId,
            Content = updateCommentDTO.Content,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("get-recipe-steps")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<Step>), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetRecipeSteps([FromBody] GetRecipeStepsDTO getRecipeStepsDTO)
    {
        var result = await _sender.Send(new GetRecipeStepsQuery
        {
            RecipeId = getRecipeStepsDTO.RecipeId,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("bookmark-recipe")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(BookmarkRecipeResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> BookmarkRecipe([FromBody] BookmarkRecipeDTO bookmarkRecipeDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new BookmarkRecipeCommand
        {
            AccountId = Guid.Parse(subjectId!),
            RecipeId = bookmarkRecipeDTO.RecipeId,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("get-recipe-bookmarks")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedRecipeFeedsListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetRecipeBookmarks([FromBody] GetRecipeBookmarkDTO getRecipeBookmarkDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new GetRecipeBookmarksQuery
        {
            AccountId = Guid.Parse(subjectId!),
            Skip = getRecipeBookmarkDTO.Skip
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("get-recipe-bin")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedRecipeFeedsListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetRecipeBins([FromBody] GetUserRecipeBinsDTO getUserRecipeBinsDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new GetUserRecipeBinsQuery
        {
            AccountId = Guid.Parse(subjectId!),
            Skip = getUserRecipeBinsDTO.Skip
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("user-report-recipe")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserReportRecipeResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> UserReportRecipe([FromBody] UserReportRecipeDTO userReportRecipeDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new UserReportRecipeCommand
        {
            ReporterId = Guid.Parse(subjectId!),
            RecipeId = userReportRecipeDTO.RecipeId,
            ReasonCodes = userReportRecipeDTO.ReasonCodes,
            AdditionalDetails = userReportRecipeDTO.AdditionalDetails,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("user-report-comment")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserReportCommentResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> UserReportComment([FromBody] UserReportCommentDTO userReportCommentDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new UserReportCommentCommand
        {
            ReporterId = Guid.Parse(subjectId!),
            CommentId = userReportCommentDTO.CommentId,
            ReasonCodes = userReportCommentDTO.ReasonCodes,
            AdditionalDetails = userReportCommentDTO.AdditionalDetails,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("create-user-search-recipe")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(string), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> CreateUserSearchRecipe([FromBody] CreateUserSearchRecipeDTO createUserSearchRecipeDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new PublishUserSearchRecipeCommand
        {
            AccountId = Guid.Parse(subjectId!),
            Keyword = createUserSearchRecipeDTO.Keyword
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("get-report-reasons")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ReportReasonResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetReportReasons([FromBody] GetReportReasonsDTO getReportReasonsDTO)
    {
        var result = await _sender.Send(new GetReportReasonsQuery
        {
            Language = getReportReasonsDTO.Language,
            ReportType = getReportReasonsDTO.ReportType,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }
}