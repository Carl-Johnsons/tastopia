using Contract.Constants;
using Duende.IdentityServer.Extensions;
using IdentityService.Application.Account.Commands;
using IdentityService.Application.Account.Queries;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
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
    [HttpPost("register/{method}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(JsonElement), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> RegisterAccount([FromRoute] string method, [FromBody] RegisterAccountDTO dto)
    {
        if (!Enum.TryParse(method, ignoreCase: true, out AccountMethod accountMethod))
        {
            return BadRequest("Invalid account method");
        }

        var command = _mapper.Map<RegisterAccountCommand>(dto);
        command.Method = accountMethod;
        var result = await _sender.Send(command);
        result.ThrowIfFailure();
        return Ok(JToken.Parse(result.Value?.Json.GetRawText()!));
    }

    [HttpPost("verify/{method}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> VerifyAccount([FromRoute] string method, [FromBody] VerifyAccountDTO dto)
    {
        if (!Enum.TryParse(method, ignoreCase: true, out AccountMethod accountMethod))
        {
            return BadRequest("Invalid account method");
        }

        var command = _mapper.Map<VerifyAccountCommand>(dto);
        var userId = _httpContextAccessor.HttpContext?.User.GetSubjectId();
        command.Method = accountMethod;
        command.AccountId = Guid.Parse(userId!);

        var result = await _sender.Send(command);
        result.ThrowIfFailure();
        return NoContent();
    }

    [HttpPost("resend/{method}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> ResendOTP([FromRoute] string method)
    {
        if (!Enum.TryParse(method, ignoreCase: true, out AccountMethod accountMethod))
        {
            return BadRequest("Invalid account method");
        }

        var command = new ResendOTPCommand();
        var userId = _httpContextAccessor.HttpContext?.User.GetSubjectId();

        command.Method = accountMethod;
        command.AccountId = Guid.Parse(userId!);

        var result = await _sender.Send(command);
        result.ThrowIfFailure();
        return NoContent();
    }

    [HttpPost("link/{method}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> LinkAccount([FromRoute] string method, AccountIdentifierDTO dto)
    {
        if (!Enum.TryParse(method, ignoreCase: true, out AccountMethod accountMethod))
        {
            return BadRequest("Invalid account method");
        }

        var userId = _httpContextAccessor.HttpContext?.User.GetSubjectId();
        var command = new LinkAccountCommand
        {
            Identifier = dto.Identifier,
            Method = accountMethod,
            Id = Guid.Parse(userId!)
        };

        var result = await _sender.Send(command);
        result.ThrowIfFailure();
        return NoContent();
    }

    [HttpPost("unlink/{method}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UnlinkAccount([FromRoute] string method)
    {
        if (!Enum.TryParse(method, ignoreCase: true, out AccountMethod accountMethod))
        {
            return BadRequest("Invalid account method");
        }

        var userId = _httpContextAccessor.HttpContext?.User.GetSubjectId();
        var command = new UnlinkAccountCommand
        {
            Method = accountMethod,
            Id = Guid.Parse(userId!)
        };

        var result = await _sender.Send(command);
        result.ThrowIfFailure();
        return NoContent();
    }

    [AllowAnonymous]
    [HttpPost("forgot-password/{method}/check")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CheckForgotPassword([FromRoute] string method, CheckForgotPasswordDTO dto)
    {
        if (!Enum.TryParse(method, ignoreCase: true, out AccountMethod accountMethod))
        {
            return BadRequest("Invalid account method");
        }

        var query = new CheckForgotPasswordOTPQuery
        {
            Identifier = dto.Identifier,
            Method = accountMethod,
            OTP = dto.OTP
        };

        var result = await _sender.Send(query);
        result.ThrowIfFailure();
        return NoContent();
    }

    [AllowAnonymous]
    [HttpPost("change-password/{method}/request")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> RequestChangePassword([FromRoute] string method, AccountIdentifierDTO dto)
    {
        if (!Enum.TryParse(method, ignoreCase: true, out AccountMethod accountMethod))
        {
            return BadRequest("Invalid account method");
        }

        var command = new RequestChangePasswordCommand
        {
            Identifier = dto.Identifier,
            Method = accountMethod,
        };

        var result = await _sender.Send(command);
        result.ThrowIfFailure();
        return NoContent();
    }

    [AllowAnonymous]
    [HttpPost("change-password/{method}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> ChangePassword([FromRoute] string method, ChangePasswordDTO dto)
    {
        if (!Enum.TryParse(method, ignoreCase: true, out AccountMethod accountMethod))
        {
            return BadRequest("Invalid account method");
        }

        var command = new ChangePasswordCommand
        {
            Identifier = dto.Identifier,
            Method = accountMethod,
            OTP = dto.OTP,
            Password = dto.Password
        };

        var result = await _sender.Send(command);
        result.ThrowIfFailure();
        return NoContent();
    }

    [AllowAnonymous]
    [HttpPost("find-account/{method}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> FindAccount([FromRoute] string method, AccountIdentifierDTO dto)
    {
        if (!Enum.TryParse(method, ignoreCase: true, out AccountMethod accountMethod))
        {
            return BadRequest("Invalid account method");
        }

        var query = new FindAccountQuery
        {
            Identifier = dto.Identifier,
            Method = accountMethod,
        };

        var result = await _sender.Send(query);
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("request-update-identifier/{method}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> RequestUpdateIdentifier([FromRoute] string method, AccountIdentifierDTO dto)
    {
        if (!Enum.TryParse(method, ignoreCase: true, out AccountMethod accountMethod))
        {
            return BadRequest("Invalid account method");
        }
        var userId = _httpContextAccessor.HttpContext?.User.GetSubjectId();

        var command = new RequestUpdateIdentifierCommand
        {
            Id = Guid.Parse(userId!),
            Identifier = dto.Identifier,
            Method = accountMethod
        };

        var result = await _sender.Send(command);
        result.ThrowIfFailure();
        return Ok();
    }

    [HttpPost("check-update-identifier/{method}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CheckUpdateIdentifier([FromRoute] string method, VerifyUpdateIdentifierDTO dto)
    {
        if (!Enum.TryParse(method, ignoreCase: true, out AccountMethod accountMethod))
        {
            return BadRequest("Invalid account method");
        }
        var userId = _httpContextAccessor.HttpContext?.User.GetSubjectId();

        var command = new CheckUpdateIdentifierOTPQuery
        {
            Identifier = dto.Identifier,
            Method = accountMethod,
            OTP = dto.OTP,
            AccountId = Guid.Parse(userId!)
        };

        var result = await _sender.Send(command);
        result.ThrowIfFailure();
        return Ok();
    }

    [HttpPost("verify-update-identifier/{method}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> VerifyUpdateIdentifier([FromRoute] string method, VerifyUpdateIdentifierDTO dto)
    {
        if (!Enum.TryParse(method, ignoreCase: true, out AccountMethod accountMethod))
        {
            return BadRequest("Invalid account method");
        }
        var userId = _httpContextAccessor.HttpContext?.User.GetSubjectId();

        var command = new VerifyUpdateIdentifierCommand
        {
            AccountId = Guid.Parse(userId!),
            OTP = dto.OTP,
            Identifier = dto.Identifier,
            Method = accountMethod
        };

        var result = await _sender.Send(command);
        result.ThrowIfFailure();
        return Ok();
    }
}
