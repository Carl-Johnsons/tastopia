using Contract.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeService.API.DTOs;
using RecipeService.Application.Reports.Commands;
using System.IdentityModel.Tokens.Jwt;
using UserService.API.DTOs;
using UserService.Application.UserReports.Queries;
using UserService.Application.Users.Commands;
using UserService.Application.Users.Queries;
using UserService.Domain.Entities;
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

    [HttpGet("get-user-reports")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedAdminUserReportListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminGetRecipeReports([FromQuery] GetReportReasonsDTO getReportReasonsDTO, [FromQuery] PaginatedDTO paginatedDTO)
    {
        var result = await _sender.Send(new GetUserReportsQuery
        {
            Lang = getReportReasonsDTO.Language,
            paginatedDTO = paginatedDTO
        });

        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("get-user-report-by-account-id")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedAdminUserReportDetailListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminGetRecipeReportDetail([FromBody] AdminGetUserReportByAccountIdDTO adminGetUserReportByAccountIdDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var result = await _sender.Send(new GetUserReportDetailByAccountIdQuery
        {
            CurrentAccountId = Guid.Parse(subjectId!),
            AccountId = adminGetUserReportByAccountIdDTO.AccountId,
            Lang = adminGetUserReportByAccountIdDTO.Language ?? "en",
            Skip = adminGetUserReportByAccountIdDTO.Skip
        });

        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("mark-report-complete")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserReport), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminMarkReportComplete([FromBody] ReportDTO reportDTO)
    {
        var result = await _sender.Send(new MarkReportCompleteCommand
        {
            ReportId = reportDTO.ReportId,
        });

        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("reopen-report")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(UserReport), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminReopenReport([FromBody] ReportDTO reportDTO)
    {
        var result = await _sender.Send(new ReopenReportCommand
        {
            ReportId = reportDTO.ReportId,
        });

        result.ThrowIfFailure();
        return Ok(result.Value);
    }
}
