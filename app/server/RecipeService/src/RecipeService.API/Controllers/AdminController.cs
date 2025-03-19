using AutoMapper;
using Contract.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeService.API.DTOs;
using RecipeService.Application.Activities;
using RecipeService.Application.Recipes.Queries;
using RecipeService.Application.Reports.Commands;
using RecipeService.Application.Reports.Queries;
using RecipeService.Domain.Responses;
using System.IdentityModel.Tokens.Jwt;

namespace RecipeService.API.Controllers;
[Route("api/admin/recipe")]
[ApiController]
[Authorize]
public class AdminController : BaseApiController
{
    private readonly IMapper _mapper;
    public AdminController(ISender sender, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(sender, httpContextAccessor)
    {
        _mapper = mapper;
    }

    [HttpGet("get-recipes")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedAdminRecipeListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminGetRecipes([FromQuery] PaginatedDTO paginatedDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var result = await _sender.Send(new AdminGetRecipesQuery
        {
            AccountId = Guid.Parse(subjectId!),
            paginatedDTO = paginatedDTO,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpGet("get-recipe-reports")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedAdminReportRecipeListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminGetRecipeReports([FromQuery] string? lang, [FromQuery] PaginatedDTO paginatedDTO)
    {
        var result = await _sender.Send(new GetRecipeReportsQuery
        {
            Lang = lang ?? "en",
            paginatedDTO = paginatedDTO
        });

        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpGet("get-recipe-report-detail")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(AdminReportRecipeDetailResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminGetRecipeReportDetail([FromQuery] string? lang, [FromQuery] Guid recipeId)
    {
        var result = await _sender.Send(new GetRecipeReportDetailQuery
        {
            Lang = lang ?? "en",
            RecipeId = recipeId
        });

        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("mark-report-complete")]
    [Produces("application/json")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminMarkReportComplete([FromBody] ReportDTO dto)
    {
        var result = await _sender.Send(new MarkReportCompleteCommand
        {
            ReportId = dto.ReportId,
            ReportType = dto.ReportType
        });

        result.ThrowIfFailure();
        return NoContent();
    }

    [HttpPost("reopen-report")]
    [Produces("application/json")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminReopenReport([FromBody] ReportDTO dto)
    {
        var result = await _sender.Send(new ReopenReportCommand
        {
            ReportId = dto.ReportId,
            ReportType = dto.ReportType
        });

        result.ThrowIfFailure();
        return NoContent();
    }

    [HttpDelete()]
    [Produces("application/json")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminDisableRecipe([FromQuery] EntityIdDTO dto)
    {
        var result = await _sender.Send(new DisableRecipeCommand
        {
            Id = dto.Id
        });

        result.ThrowIfFailure();
        return NoContent();
    }

    [HttpPut("restore")]
    [Produces("application/json")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminRestoreRecipe([FromQuery] EntityIdDTO dto)
    {
        var result = await _sender.Send(new RestoreEntityCommand
        {
            Id = dto.Id
        });

        result.ThrowIfFailure();
        return NoContent();
    }

    [HttpGet("comment/reports/all")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedAccountRecipeCommentListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminGetCommentReports([FromQuery] string? lang, [FromQuery] PaginatedDTO paginatedDTO)
    {
        var result = await _sender.Send(new GetCommentReportsQuery
        {
            Lang = lang ?? "en",
            PaginatedDTO = paginatedDTO
        });

        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpGet("comment/reports")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(AdminReportCommentDetailResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminGetCommentReports([FromQuery] string? lang, [FromQuery] Guid recipeId, [FromQuery] Guid commentId)
    {
        var result = await _sender.Send(new GetCommentReportDetailQuery
        {
            Lang = lang ?? "en",
            CommentId = commentId,
            RecipeId = recipeId
        });

        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("get-user-activities")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedUserActivityListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminGetUserActivities([FromBody] AdminGetUserActivityDTO adminGetUserActivityDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var result = await _sender.Send(new AdminGetUserActivityQuery
        {
            CurrentAccountId = Guid.Parse(subjectId!),
            AccountId = adminGetUserActivityDTO.AccountId,
            Skip = adminGetUserActivityDTO.Skip,
            Language = adminGetUserActivityDTO.Language,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }
}