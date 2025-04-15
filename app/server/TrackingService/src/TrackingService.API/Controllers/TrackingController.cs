using Contract.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TrackingService.API.DTOs;
using TrackingService.Application.UserSearchRecipes.Queries;
using TrackingService.Application.UserSearchUsers.Commands;
using TrackingService.Application.UserSearchUsers.Queries;
using TrackingService.Application.UserViewRecipeDetails.Commands;
using TrackingService.Application.UserViewRecipeDetails.Queries;
using TrackingService.Domain.Entities;
using TrackingService.Domain.Responses;
namespace TrackingService.API.Controllers;
[Route("api/tracking")]
[ApiController]
[Authorize]
public class TrackingController : BaseApiController
{
    public TrackingController(ISender sender, IHttpContextAccessor httpContextAccessor) : base(sender, httpContextAccessor)
    {
    }

    [HttpPost("get-user-view-recipe-detail-history")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedUserViewRecipeDetailListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetUserViewRecipeDetailHistory([FromBody] GetUserViewRecipeDetailHistoryDTO getUserViewRecipeDetailHistoryDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var result = await _sender.Send(new GetUserViewRecipeDetaiQuery
        {
            AccountId = Guid.Parse(subjectId!),
            Skip = getUserViewRecipeDetailHistoryDTO.Skip,
        });
        result.ThrowIfFailure();
        return Ok(result);
    }

    [HttpPost("search-user-view-recipe-detail-history")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedUserViewRecipeDetailListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> SearchUserViewRecipeDetailHistory([FromBody] SearchUserViewRecipeDetailHistoryDTO searchUserViewRecipeDetailHistoryDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var result = await _sender.Send(new SearchUserViewRecipeDetaiQuery
        {
            AccountId = Guid.Parse(subjectId!),
            Skip = searchUserViewRecipeDetailHistoryDTO.Skip,
            Keyword = searchUserViewRecipeDetailHistoryDTO.Keyword
        });
        result.ThrowIfFailure();
        return Ok(result);
    }

    [HttpGet("get-user-search-recipe-history")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<string>), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetUserSearchRecipe()
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var result = await _sender.Send(new GetUserSearchRecipeQuery
        {
            AccountId = Guid.Parse(subjectId!),
        });
        result.ThrowIfFailure();
        return Ok(result);
    }

    [HttpGet("get-user-search-user-history")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<string>), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetUserSearchUser()
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var result = await _sender.Send(new GetUserSearchUserQuery
        {
            AccountId = Guid.Parse(subjectId!),
        });
        result.ThrowIfFailure();
        return Ok(result);
    }

    [HttpPost("delete-user-search-user-history")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<UserSearchUser>), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> DeleteUserSearchUser([FromBody] DeleteUserSearchUserKeywordDTO deleteUserSearchUserKeywordDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var result = await _sender.Send(new DeleteUserSearchUserCommand
        {
            AccountId = Guid.Parse(subjectId!),
            Keyword = deleteUserSearchUserKeywordDTO.Keyword,
        });
        result.ThrowIfFailure();
        return Ok(result);
    }

    [HttpPost("delete-user-search-recipe-history")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<UserSearchRecipe>), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> DeleteUserSearchRecipe([FromBody] DeleteUserSearchRecipeKeywordDTO deleteUserSearchRecipeKeywordDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var result = await _sender.Send(new DeleteUserSearchRecipeCommand
        {
            AccountId = Guid.Parse(subjectId!),
            Keyword = deleteUserSearchRecipeKeywordDTO.Keyword,
        });
        result.ThrowIfFailure();
        return Ok(result);
    }
}
