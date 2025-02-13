using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using UserService.API.DTOs;
using UserService.Application.Users.Commands;
using UserService.Application.Users.Queries;
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
    [ProducesResponseType(typeof(PaginatedSimpleUserListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> SearchUser([FromBody] SearchUserDTO searchUser)
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

    [AllowAnonymous]
    [HttpPost("get-user-detail-by-account-id")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(GetUserDetailsResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetUserDetailByAccountId([FromBody] GetUserDetailByAccountIdDTO getUserDetailByAccountIdDTO)
    {

        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var currentAccountId = Guid.Empty;

        if (!string.IsNullOrEmpty(subjectId))
        {
            currentAccountId = Guid.Parse(subjectId);
        }

        var result = await _sender.Send(new GetUserDetailsQuery
        {
            AccountId = getUserDetailByAccountIdDTO.AccountId,
            CurrentAccountId = currentAccountId
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("get-user-follower")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedSimpleUserListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetUserFollower([FromBody] GetUserFollowersDTO getUserFollowersDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new GetUserFollowersQuery
        {
            AccountId = subjectId != null ? Guid.Parse(subjectId) : null,
            Skip = getUserFollowersDTO.Skip,
            Keyword = getUserFollowersDTO.Keyword,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("get-user-following")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedSimpleUserListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetUserFollowing([FromBody] GetUserFollowingsDTO getUserFollowingsDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new GetUserFollowingsQuery
        {
            AccountId = subjectId != null ? Guid.Parse(subjectId) : null,
            Skip = getUserFollowingsDTO.Skip,
            Keyword = getUserFollowingsDTO.Keyword,
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

        var result = await _sender.Send(new GetUserDetailsQuery
        {
            AccountId = subjectId != null ? Guid.Parse(subjectId) : Guid.Empty,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("follow-user")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(FollowUserResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> FollowUser([FromBody] FollowUserDTO followUserDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new FollowUserCommand
        {
            AccountId = subjectId != null ? Guid.Parse(subjectId) : Guid.Empty,
            FollowingId = followUserDTO.AccountId,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPatch()]
    [Produces("application/json")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> UpdateUser([FromForm] UpdateUserDTO dto)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new UpdateUserCommand
        {
            AccountId = subjectId != null ? Guid.Parse(subjectId) : Guid.Empty,
            Avatar = dto.Avatar,
            Username = dto.Username,
            Background = dto.Background,
            DisplayName = dto.DisplayName,
            Gender = dto.Gender,
            Bio = dto.Bio
        });

        result.ThrowIfFailure();
        return NoContent();
    }
}
