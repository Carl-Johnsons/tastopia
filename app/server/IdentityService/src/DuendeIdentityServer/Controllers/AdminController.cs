using IdentityService.Application.Account.Queries;
using Microsoft.AspNetCore.Authorization;
using static Duende.IdentityServer.IdentityServerConstants;
namespace DuendeIdentityServer.Controllers;
[Route("api/admin/account")]
[ApiController]
[Authorize(LocalApi.PolicyName)]

public class AdminController : BaseApiController
{
    public AdminController(ISender sender, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(sender, httpContextAccessor, mapper)
    {
    }

    [HttpGet("get-account-statistic")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<StatisticEntity>), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetAccountStatistic()
    {
        var result = await _sender.Send(new AdminGetNumberOfAccountStatisticQuery
        {
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

}
