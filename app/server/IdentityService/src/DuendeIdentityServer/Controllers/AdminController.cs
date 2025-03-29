using IdentityService.Application.Account.Commands;
using IdentityService.Application.Account.Queries;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
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

    [HttpPost()]
    [Produces("application/json")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> CreateAdmin([FromForm] CreateAdminAccountDTO dto)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var result = await _sender.Send(new CreateAdminAccountCommand
        {
            CurrentAccountId = Guid.Parse(subjectId!),
            Address = dto.Address,
            AvatarFile = dto.AvatarFile,
            Gender = dto.Gender,
            Dob = dto.Dob,
            Gmail = dto.Gmail,
            Name = dto.Name,
            Password = dto.Password,
            Phone = dto.Phone,
        });
        result.ThrowIfFailure();
        return NoContent();
    }

    [HttpPatch]
    [Produces("application/json")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> UpdateAdmin([FromForm] UpdateAdminAccountDTO dto)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var result = await _sender.Send(new UpdateAdminAccountCommand
        {
            CurrentAccountId = Guid.Parse(subjectId!),
            Address = dto.Address,
            AvatarFile = dto.AvatarFile,
            Username = dto.Username,
            Gender = dto.Gender,
            Dob = dto.Dob,
            Gmail = dto.Gmail,
            Name = dto.Name,
            AccountId = dto.AccountId,
            Phone = dto.Phone,
        });
        result.ThrowIfFailure();
        return NoContent();
    }
}
