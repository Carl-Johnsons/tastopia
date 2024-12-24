using Contract.Constants;
using Duende.IdentityServer.Extensions;
using IdentityService.Application.Account;
using Microsoft.AspNetCore.Authorization;
using static Duende.IdentityServer.IdentityServerConstants;

namespace DuendeIdentityServer.Controllers;

[Route("api/account")]
[ApiController]
[Authorize(LocalApi.PolicyName)]
public class AccountController : BaseApiController
{
    public AccountController(ISender sender, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(sender, httpContextAccessor, mapper)
    {
    }

    [AllowAnonymous]
    [HttpPost("register/email")]
    public async Task<IActionResult> RegisterWithEmail(RegisterAccountDTO dto)
    {
        var command = _mapper.Map<RegisterAccountCommand>(dto);
        command.Method = AccountMethod.Email;
        var result = await _sender.Send(command);
        result.ThrowIfFailure();
        return Ok(result.Value?.Json);
    }

    [AllowAnonymous]
    [HttpPost("register/phone")]
    public async Task<IActionResult> RegisterWithPhone(RegisterAccountDTO dto)
    {
        var command = _mapper.Map<RegisterAccountCommand>(dto);
        command.Method = AccountMethod.Phone;
        var result = await _sender.Send(command);
        result.ThrowIfFailure();
        return Ok(result.Value?.Json);
    }


    [HttpPost("verify/email")]
    public async Task<IActionResult> VerifyEmail(VerifyAccountDTO dto)
    {
        var command = _mapper.Map<VerifyAccountCommand>(dto);
        var userId = _httpContextAccessor.HttpContext?.User.GetSubjectId();
        command.Method = AccountMethod.Email;
        command.AccountId = Guid.Parse(userId!);

        var result = await _sender.Send(command);
        result.ThrowIfFailure();
        return Ok();
    }

    [HttpPost("verify/phone")]
    public async Task<IActionResult> VerifyPhone(VerifyAccountDTO dto)
    {
        var command = _mapper.Map<VerifyAccountCommand>(dto);
        var userId = _httpContextAccessor.HttpContext?.User.GetSubjectId();

        command.Method = AccountMethod.Phone;
        command.AccountId = Guid.Parse(userId!);

        var result = await _sender.Send(command);
        result.ThrowIfFailure();
        return Ok();
    }
}
