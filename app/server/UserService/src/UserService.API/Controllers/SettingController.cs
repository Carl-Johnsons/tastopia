using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using UserService.API.DTOs;
using UserService.Application.Settings.Commands;
using UserService.Application.Settings.Queries;
using UserService.Domain.Entities;

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

        var settingsObj = new List<SettingObject>();

        foreach (var settingDTO in dto.Settings)
        {
            settingsObj.Add(new SettingObject
            {
                Key = settingDTO.Key,
                Value = settingDTO.Value
            });
        }

        var result = await _sender.Send(new UpdateSettingCommand
        {
            AccountId = Guid.Parse(subjectId!),
            Settings = settingsObj
        });
        result.ThrowIfFailure();
        return NoContent();
    }

    [HttpGet()]
    [ProducesResponseType(typeof(List<UserSetting>), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetSetting()
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new GetUserSettingQuery
        {
            AccountId = Guid.Parse(subjectId!),
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }
}
