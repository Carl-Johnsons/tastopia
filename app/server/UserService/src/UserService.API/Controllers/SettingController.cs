using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using UserService.API.DTOs;
using UserService.Application.Settings.Commands;

namespace UserService.API.Controllers;

[Route("api/setting")]
[ApiController]
[Authorize]
public class SettingController : BaseApiController
{
    public SettingController(ISender sender, IHttpContextAccessor httpContextAccessor) : base(sender, httpContextAccessor)
    {
    }

    [HttpPut()]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> UpdateSetting([FromBody] UpdateSettingDTO dto)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new UpdateSettingCommand
        {
            AccountId = Guid.Parse(subjectId!),
            Settings = dto.Settings
        });
        result.ThrowIfFailure();
        return NoContent();
    }
}
