using Contract.DTOs;
using Microsoft.AspNetCore.Authorization;
using NotificationService.API.DTOs;
using NotificationService.Application.Notifications.Commands;
using NotificationService.Application.Notifications.Queries;
using NotificationService.Domain.Constants;
using NotificationService.Domain.Entities;
using NotificationService.Domain.Responses;
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
    [ProducesResponseType(201)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
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
    [ProducesResponseType(201)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
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
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
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
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
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
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
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
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedNotificationListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetNotifications(int? skip, string? lang, string? type)
    {
        type ??= NotificationCategories.ALL.ToString();
        if (!Enum.TryParse(typeof(NotificationCategories), type, out var category))
        {
            return BadRequest("The type must be ALL, USER, SYSTEM");
        }

        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var result = await _sender.Send(new GetNotificationsQuery
        {
            AccountId = Guid.Parse(subjectId!),
            Skip = skip,
            Language = lang ?? "en",
            Category = (NotificationCategories)category
        });

        result.ThrowIfFailure();

        return Ok(result.Value);
    }

    [HttpPost("set-view-notification")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Recipient), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> SetViewNotification([FromBody] SetViewNotifyDTO setViewNotifyDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var result = await _sender.Send(new SetViewNotificationCommand
        {
            AccountId = Guid.Parse(subjectId!),
            NotificationId = setViewNotifyDTO.NotificationId,
        });

        result.ThrowIfFailure();
        return Ok(result.Value);
    }
}
