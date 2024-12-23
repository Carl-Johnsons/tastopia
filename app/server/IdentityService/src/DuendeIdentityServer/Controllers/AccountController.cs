using IdentityService.Application.Account;

namespace DuendeIdentityServer.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : BaseApiController
{
    public AccountController(ISender sender, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(sender, httpContextAccessor, mapper)
    {
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterAccountDTO dto)
    {
        var command = await _sender.Send(_mapper.Map<RegisterAccountCommand>(dto));
        command.ThrowIfFailure();
        return Ok();
    }
}
