using Contract.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeService.API.DTOs;
using RecipeService.Application.Recipes.Queries;
using RecipeService.Application.Reports.Commands;
using System.IdentityModel.Tokens.Jwt;
using UserService.API.DTOs;
using UserService.Application.UserReports.Queries;
using UserService.Application.Users.Commands;
using UserService.Application.Users.Queries;
using UserService.Domain.Responses;
using ErrorResponseDTO = UserService.API.DTOs.ErrorResponseDTO;

namespace UserService.API.Controllers;

[Route("api/admin/user")]
[ApiController]
[Authorize]
public class AdminController : BaseApiController
{
    public AdminController(ISender sender, IHttpContextAccessor httpContextAccessor) : base(sender, httpContextAccessor)
    {
    }

    [HttpPost("get-user-detail")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(AdminGetUserDetailResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminGetUserDetail([FromBody] GetUserDetailByAccountIdDTO getUserDetailByAccountIdDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var result = await _sender.Send(new AdminGetUserDetailQuery
        {
            CurrentAccountId = Guid.Parse(subjectId!),
            AccountId = getUserDetailByAccountIdDTO.AccountId,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpGet("get-users")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedAdminGetUserListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminGetUsers([FromQuery] PaginatedDTO paginatedDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var result = await _sender.Send(new AdminGetUsersQuery
        {
            AccountId = Guid.Parse(subjectId!),
            PaginatedDTO = paginatedDTO
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("ban-user")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(AdminBanUserResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminBanUser([FromBody] AdminBanUserDTO adminBanUserDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var result = await _sender.Send(new AdminBanUserCommand
        {
            CurrentAccountId = Guid.Parse(subjectId!),
            AccountId = adminBanUserDTO.AccountId,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("toggle-admin-active")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(AdminBanUserResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> BanAdmin([FromBody] AdminBanUserDTO adminBanUserDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var result = await _sender.Send(new BanAdminCommand
        {
            CurrentAccountId = Guid.Parse(subjectId!),
            AccountId = adminBanUserDTO.AccountId,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpGet("get-user-reports")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedAdminUserReportListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminGetUserReports([FromQuery] GetReportReasonsDTO getReportReasonsDTO, [FromQuery] PaginatedDTO paginatedDTO)
    {
        var result = await _sender.Send(new GetUserReportsQuery
        {
            Lang = getReportReasonsDTO.Language,
            paginatedDTO = paginatedDTO,
        });

        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("get-user-report-by-account-id")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedAdminUserReportDetailListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminGetUserReportDetail([FromBody] AdminGetUserReportByAccountIdDTO adminGetUserReportByAccountIdDTO)
    {
        var result = await _sender.Send(new GetUserReportDetailByAccountIdQuery
        {
            AccountId = adminGetUserReportByAccountIdDTO.AccountId,
            Lang = adminGetUserReportByAccountIdDTO.Language ?? "en",
            Skip = adminGetUserReportByAccountIdDTO.Skip
        });

        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("mark-report")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(AdminMarkReportResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminMarkReport([FromBody] ReportDTO reportDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var result = await _sender.Send(new MarkReportCommand
        {
            ReportId = reportDTO.ReportId,
            CurrentAccountId = Guid.Parse(subjectId!)
        });

        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("mark-all-user-report")]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminMarkAllReport([FromBody] MarkAllUserReportDTO markAllUserReportDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var result = await _sender.Send(new MarkAllReportCommand
        {
            AccountId = markAllUserReportDTO.AccountId,
            IsReopened = markAllUserReportDTO.IsReopened,
            CurrentAccountId = Guid.Parse(subjectId!)
        });

        result.ThrowIfFailure();
        return NoContent();
    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedAdminListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetAdmins([FromQuery] PaginatedDTO paginatedDTO)
    {
        var result = await _sender.Send(new GetAdminsQuery
        {
            DTO = paginatedDTO
        });

        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpGet("current")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(AdminDetailResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetAdminDetail()
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var result = await _sender.Send(new GetAdminDetailQuery
        {
            AccountId = Guid.Parse(subjectId!)
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpGet("detail")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(AdminDetailResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetAdminDetail([FromQuery] Guid id)
    {
        var result = await _sender.Send(new GetAdminDetailQuery
        {
            AccountId = id
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpGet("statistic/get-total-user")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(int), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetTotalUser()
    {
        var result = await _sender.Send(new AdminGetTotalUserNumberQuery
        {
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }
}
