using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostService.Application.Posts.Commands;
using PostService.Application.Posts.Queries;
using PostService.Domain.DTOs;
using System.IdentityModel.Tokens.Jwt;

namespace PostService.API.Controllers;

[Route("api/content-restriction")]
[ApiController]
[Authorize]
public class UserContentRestrictionController : BaseApiController
{
    public UserContentRestrictionController(ISender sender, IHttpContextAccessor httpContextAccessor) : base(sender, httpContextAccessor)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateContentRestrictionDTO contentRestrictionDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new CreateUserContentRestrictionsCommand
        {
            AdminId = Guid.Parse(subjectId!),
            UserId = contentRestrictionDTO.UserId,
            TypeId = contentRestrictionDTO.TypeId,
            ExpiredAt = contentRestrictionDTO.ExpiredAt
        });

        result.ThrowIfFailure();

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateContentRestrictionDTO updateContentRestrictionDTO)
    {
        var result = await _sender.Send(new UpdateUserContentRestrictionsCommand
        {
            Id = updateContentRestrictionDTO.Id,
            UserId = updateContentRestrictionDTO.UserId,
            TypeId = updateContentRestrictionDTO.TypeId,
            ExpiredAt = updateContentRestrictionDTO.ExpiredAt
        });

        result.ThrowIfFailure();

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteContentRestrictionDTO deleteContentRestrictionDTO)
    {
        var result = await _sender.Send(new DeleteUserContentRestrictionsCommand
        {
            Id = deleteContentRestrictionDTO.Id
        });
        
        result.ThrowIfFailure();

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] Guid UserId)
    {
        var result = await _sender.Send(new GetUserContentRestrictionsByUserIdQuery
        {
            UserId = UserId
        });

        result!.ThrowIfFailure();

        return Ok(result.Value);
    }
}
