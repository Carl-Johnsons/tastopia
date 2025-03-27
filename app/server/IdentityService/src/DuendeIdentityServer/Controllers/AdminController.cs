using IdentityService.Application.Account.Queries;
using Microsoft.AspNetCore.Authorization;
namespace DuendeIdentityServer.Controllers;
[Route("api/admin/account")]
[ApiController]
[Authorize]
public class AdminController : BaseApiController
{
    public AdminController(ISender sender, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(sender, httpContextAccessor, mapper)
    {
    }

    [HttpPost("get-account-statistic")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<StatisticEntity>), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetAccountStatistic([FromBody] AdminGetAccountStatisticDTO adminGetAccountStatisticDTO)
    {
        var result = await _sender.Send(new AdminGetNumberOfAccountStatisticQuery
        {
            Language = adminGetAccountStatisticDTO.Language,
            RangeType = adminGetAccountStatisticDTO.RangeType,
        });

        result.ThrowIfFailure();
        return Ok(result.Value);
    }

}
