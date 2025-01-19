using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using TrackingService.API.DTOs;
using TrackingService.Application.UserViewRecipeDetails.Queries;

namespace TrackingService.API.Controllers;

[Route("api/tracking")]
[ApiController]
[Authorize]
public class TrackingController : BaseApiController
{
    public TrackingController(ISender sender, IHttpContextAccessor httpContextAccessor) : base(sender, httpContextAccessor)
    {
    }

    [HttpPost("get-user-view-recipe-detail-history")]
    [Produces("application/json")]
    public async Task<IActionResult> GetUserViewRecipeDetailHistory([FromBody] GetUserViewRecipeDetailHistoryDTO getUserViewRecipeDetailHistoryDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        await Console.Out.WriteLineAsync("subjectId:" + string.IsNullOrEmpty(subjectId));

        var result = await _sender.Send(new GetUserViewRecipeDetaiQuery
        {
            AccountId = Guid.Parse(subjectId!),
        });
        result.ThrowIfFailure();
        return Ok(result);
    }
}
