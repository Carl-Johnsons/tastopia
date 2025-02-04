using Microsoft.AspNetCore.Authorization;
using NotificationService.API.DTOs;
using NotificationService.Application.Notifications.Commands;
using NotificationService.Application.Notifications.Queries;
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
    public async Task<IActionResult> SaveExpoAndroidPushToken([FromBody] SaveExpoPushTokenDTO dto)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new SaveExpoPushTokenCommand
        {
            AccountId = Guid.Parse(subjectId!),
            DeviceType = Domain.Constants.DeviceType.ANDROID,
            ExpoPushToken = dto.ExpoPushToken,
        });
        result.ThrowIfFailure();

        return Created();
    }

    [HttpPost("expo-push-token/ios")]
    public async Task<IActionResult> SaveExpoIOSPushToken([FromBody] SaveExpoPushTokenDTO dto)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new SaveExpoPushTokenCommand
        {
            AccountId = Guid.Parse(subjectId!),
            DeviceType = Domain.Constants.DeviceType.IOS,
            ExpoPushToken = dto.ExpoPushToken,
        });
        result.ThrowIfFailure();

        return Created();
    }

    [HttpDelete("expo-push-token/android")]
    public async Task<IActionResult> RemoveExpoAndroidPushToken()
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new RemoveExpoPushTokenCommand
        {
            AccountId = Guid.Parse(subjectId!),
            Type = Domain.Constants.DeviceType.ANDROID
        });
        result.ThrowIfFailure();

        return NoContent();
    }

    [HttpDelete("expo-push-token/ios")]
    public async Task<IActionResult> RemoveExpoIOSPushToken()
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new RemoveExpoPushTokenCommand
        {
            AccountId = Guid.Parse(subjectId!),
            Type = Domain.Constants.DeviceType.IOS
        });
        result.ThrowIfFailure();

        return NoContent();
    }

    [HttpPost("notify/push")]
    public async Task<IActionResult> NotifyPush([FromBody] NotifyDTO dto)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var result = await _sender.Send(new PushNotificationCommand
        {
            Message = dto.Message,
            RecipientIds = dto.RecipientIds,
            Data = dto.Data,
            Title = dto.Title,
            ChannelId = dto.ChannelId,
        });
        result.ThrowIfFailure();

        return Ok();
    }

    [HttpGet()]
    public async Task<IActionResult> GetNotifications()
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var result = await _sender.Send(new GetNotificationsQuery
        {
            AccountId = Guid.Parse(subjectId!)
        });

        result.ThrowIfFailure();

        return Ok(result.Value);
    }
}
