using Contract.Constants;
using Duende.IdentityServer.Extensions;
using IdentityService.Application.Account.Commands;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
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
    [Produces("application/json")]
    [ProducesResponseType(typeof(JsonElement), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
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
    [Produces("application/json")]
    [ProducesResponseType(typeof(JsonElement), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> RegisterWithPhone(RegisterAccountDTO dto)
    {
        var command = _mapper.Map<RegisterAccountCommand>(dto);
        command.Method = AccountMethod.Phone;
        var result = await _sender.Send(command);
        result.ThrowIfFailure();
        return Ok(result.Value?.Json);
    }

    [HttpPost("verify/email")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> VerifyEmail(VerifyAccountDTO dto)
    {
        var command = _mapper.Map<VerifyAccountCommand>(dto);
        var userId = _httpContextAccessor.HttpContext?.User.GetSubjectId();
        command.Method = AccountMethod.Email;
        command.AccountId = Guid.Parse(userId!);

        var result = await _sender.Send(command);
        result.ThrowIfFailure();
        return NoContent();
    }

    [HttpPost("verify/phone")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> VerifyPhone(VerifyAccountDTO dto)
    {
        var command = _mapper.Map<VerifyAccountCommand>(dto);
        var userId = _httpContextAccessor.HttpContext?.User.GetSubjectId();

        command.Method = AccountMethod.Phone;
        command.AccountId = Guid.Parse(userId!);

        var result = await _sender.Send(command);
        result.ThrowIfFailure();
        return NoContent();
    }

    [HttpPost("resend/email")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> ResendEmailOTP()
    {
        var command = new ResendOTPCommand();
        var userId = _httpContextAccessor.HttpContext?.User.GetSubjectId();

        command.Method = AccountMethod.Email;
        command.AccountId = Guid.Parse(userId!);

        var result = await _sender.Send(command);
        result.ThrowIfFailure();
        return NoContent();
    }

    [HttpPost("resend/phone")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> ResendPhoneOTP()
    {
        var command = new ResendOTPCommand();
        var userId = _httpContextAccessor.HttpContext?.User.GetSubjectId();

        command.Method = AccountMethod.Phone;
        command.AccountId = Guid.Parse(userId!);

        var result = await _sender.Send(command);
        result.ThrowIfFailure();
        return NoContent();
    }

    [HttpPost("link/email")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> LinkEmailToAccount(LinkAccountDTO dto)
    {
        var userId = _httpContextAccessor.HttpContext?.User.GetSubjectId();
        var command = new LinkAccountCommand
        {
            Identifier = dto.Identifier,
            Method = AccountMethod.Email,
            Id = Guid.Parse(userId!)
        };

        var result = await _sender.Send(command);
        result.ThrowIfFailure();
        return NoContent();
    }

    [HttpPost("link/phone")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> LinkPhoneToAccount(LinkAccountDTO dto)
    {
        var userId = _httpContextAccessor.HttpContext?.User.GetSubjectId();
        var command = new LinkAccountCommand
        {
            Identifier = dto.Identifier,
            Method = AccountMethod.Phone,
            Id = Guid.Parse(userId!)
        };

        var result = await _sender.Send(command);
        result.ThrowIfFailure();
        return NoContent();
    }
}
