using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeService.Domain.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using UserService.API.DTOs;
using UserService.Application.Users.Commands;
using UserService.Domain.Responses;

namespace UserService.API.Controllers;

[Route("api/user")]
[ApiController]
[Authorize]
public class UserController : BaseApiController
{
    public UserController(ISender sender, IHttpContextAccessor httpContextAccessor) : base(sender, httpContextAccessor)
    {
    }

    [AllowAnonymous]
    [HttpPost("search")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedSearchUserListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> SearchUser([FromBody] SearchUser searchUser)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new SearchUsersCommand
        {
            AccountId = subjectId != null ? Guid.Parse(subjectId) : null,
            Skip = searchUser.Skip,
            Keyword = searchUser.Keyword,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpGet("get-current-user-details")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(GetUserDetailsResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetCurrentUserDetails()
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new GetUserDetailsCommand
        {
            AccountId = subjectId != null ? Guid.Parse(subjectId) : null,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }
}
