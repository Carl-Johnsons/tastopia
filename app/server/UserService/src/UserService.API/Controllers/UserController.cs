using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using UserService.API.DTOs;
using UserService.Application.Users.Commands;

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
