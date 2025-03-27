using Contract.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    [HttpGet()]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedUserViewRecipeDetailListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetUserViewRecipeDetailHistory([FromQuery] PaginatedDTO dto, [FromQuery] Guid accountId, [FromQuery] string? lang)
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
}
