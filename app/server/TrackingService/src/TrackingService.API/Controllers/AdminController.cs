using Contract.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TrackingService.Application.ActivityLog.Queries;
using TrackingService.Domain.Responses;
namespace TrackingService.API.Controllers;

[Route("api/admin/tracking")]
[ApiController]
[Authorize]
public class AdminController : BaseApiController
{
    public AdminController(ISender sender, IHttpContextAccessor httpContextAccessor) : base(sender, httpContextAccessor)
    {
    }

    [HttpGet("current")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedAdminActivityLogListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetCurrentAdminActivityLog([FromQuery] PaginatedDTO dto, [FromQuery] string? lang)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var result = await _sender.Send(new GetAdminActivityLogQuery
        {
            AccountId = Guid.Parse(subjectId!),
            DTO = dto,
            Lang = lang ?? "en"
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpGet()]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedAdminActivityLogListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetAdminActivityLog([FromQuery] PaginatedDTO dto, [FromQuery] Guid accountId, [FromQuery] string? lang)
    {
        var result = await _sender.Send(new GetAdminActivityLogQuery
        {
            AccountId = accountId,
            DTO = dto,
            Lang = lang ?? "en"
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpGet("all")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedAdminActivityLogListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetAllAdminActivityLog([FromQuery] PaginatedDTO dto)
    {
        var result = await _sender.Send(new GetAllAdminActivityLogQuery
        {
            DTO = dto
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }
}
