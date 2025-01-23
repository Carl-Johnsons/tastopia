using Microsoft.AspNetCore.Authorization;
using NotificationService.API.DTOs;
using NotificationService.Application.Notifications.Commands;
using System.IdentityModel.Tokens.Jwt;

namespace NotificationService.API.Controllers;

[Route("api/notification")]
[ApiController]
[Authorize]
public partial class NotificationController : BaseApiController
{
    public NotificationController(
            ISender sender,
            IHttpContextAccessor httpContextAccessor
        ) : base(sender, httpContextAccessor)
    {
    }

    [HttpPost("expo-push-token/android")]
    public async Task SaveExpoAndroidPushToken([FromBody] SaveExpoPushTokenDTO dto)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new SaveExpoPushTokenCommand
        {
            AccountId = Guid.Parse(subjectId!),
            DeviceType = Domain.Constants.DeviceType.ANDROID,
            ExpoPushToken = dto.ExpoPushToken,
        });
    }

    [HttpPost("expo-push-token/ios")]
    public async Task SaveExpoIOSPushToken([FromBody] SaveExpoPushTokenDTO dto)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new SaveExpoPushTokenCommand
        {
            AccountId = Guid.Parse(subjectId!),
            DeviceType = Domain.Constants.DeviceType.IOS,
            ExpoPushToken = dto.ExpoPushToken,
        });
    }
}
