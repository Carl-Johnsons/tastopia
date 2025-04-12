using AutoMapper;
using Contract.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeService.API.DTOs;
using RecipeService.Application.Activities;
using RecipeService.Application.Recipes.Queries;
using RecipeService.Application.Reports.Commands;
using RecipeService.Application.Reports.Queries;
using RecipeService.Application.Tags.Commands;
using RecipeService.Application.Tags.Queries;
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
        var result = await _sender.Send(new AdminGetRecipesQuery
        {
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
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new MarkReportCompleteCommand
        {
            ReportId = dto.ReportId,
            ReportType = dto.ReportType,
            CurrentAccountId = Guid.Parse(subjectId!)
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
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new ReopenReportCommand
        {
            ReportId = dto.ReportId,
            ReportType = dto.ReportType,
            CurrentAccountId = Guid.Parse(subjectId!)
        });

        result.ThrowIfFailure();
        return NoContent();
    }

    [HttpPost("mark-all-recipe-report")]
    [Produces("application/json")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminMarkAllRecipeReport([FromBody] MarkAllRecipeReportDTO markAllRecipeReportDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new MarkAllRecipeReportCommand
        {
            RecipeId = markAllRecipeReportDTO.RecipeId,
            IsReopened = markAllRecipeReportDTO.IsReopened,
            CurrentAccountId = Guid.Parse(subjectId!)
        });

        result.ThrowIfFailure();
        return NoContent();
    }

    [HttpPost("mark-all-comment-report")]
    [Produces("application/json")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminMarkAllCommentReport([FromBody] MarkAllCommentReportDTO markAllCommentReportDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new MarkAllCommentReportCommand
        {
            RecipeId = markAllCommentReportDTO.RecipeId,
            CommentId = markAllCommentReportDTO.CommentId,
            IsReopened = markAllCommentReportDTO.IsReopened,
            CurrentAccountId = Guid.Parse(subjectId!)
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
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new DisableRecipeCommand
        {
            Id = dto.Id,
            CurrentAccountId = Guid.Parse(subjectId!)
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
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;
        var result = await _sender.Send(new RestoreRecipeCommand
        {
            Id = dto.Id,
            CurrentAccountId = Guid.Parse(subjectId!)
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

    [HttpDelete("comment")]
    [Produces("application/json")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminDisableComment([FromQuery] Guid recipeId, [FromQuery] Guid commentId)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new DisableCommentCommand
        {
            CommentId = commentId,
            RecipeId = recipeId,
            CurrentAccountId = Guid.Parse(subjectId!)
        });

        result.ThrowIfFailure();
        return NoContent();
    }

    [HttpPut("comment/restore")]
    [Produces("application/json")]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminRestoreComment([FromQuery] Guid recipeId, [FromQuery] Guid commentId)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new RestoreCommentCommand
        {
            CommentId = commentId,
            RecipeId = recipeId,
            CurrentAccountId = Guid.Parse(subjectId!)
        });

        result.ThrowIfFailure();
        return NoContent();
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
            AccountId = adminGetUserActivityDTO.AccountId,
            Skip = adminGetUserActivityDTO.Skip,
            Language = adminGetUserActivityDTO.Language,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    //Tag
    [HttpPost("get-tags")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedAdminTagListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminGetTag([FromBody] AdminGetTagDTO adminGetTagDTO, [FromQuery] PaginatedDTO paginatedDTO)
    {
        var result = await _sender.Send(new AdminGetTagsQuery
        {
            PaginatedDTO = paginatedDTO,
            Lang = adminGetTagDTO.Language,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("create-tag")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(TagResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> CreateTag([FromForm] CreateTagDTO createTagDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new CreateTagCommand
        {
            Code = createTagDTO.Code,
            En = createTagDTO.En,
            Vi = createTagDTO.Vi,
            Category = createTagDTO.Category,
            TagImage = createTagDTO.TagImage,
            CurrentAccountId = Guid.Parse(subjectId!)
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("update-tag")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(TagResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> UpdateTag([FromForm] UpdateTagDTO updateTagDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new UpdateTagCommand
        {
            TagId = updateTagDTO.TagId,
            Code = updateTagDTO.Code,
            En = updateTagDTO.En,
            Vi = updateTagDTO.Vi,
            Category = updateTagDTO.Category,
            Status = updateTagDTO.Status,
            TagImage = updateTagDTO.TagImage,
            CurrentAccountId = Guid.Parse(subjectId!)
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("tag-detail")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(TagResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetTagDetail([FromBody] AdminGetTagDetailDTO adminGetTagDetailDTO)
    {
        var result = await _sender.Send(new GetTagDetailQuery
        {
            TagId = adminGetTagDetailDTO.TagId
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    // Statistic
    [HttpGet("statistic/get-recipe-statistic")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<StatisticEntity>), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetRecipeStatistic()
    {
        var result = await _sender.Send(new AdminGetNumberOfRecipesStatisticQuery
        {
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpGet("statistic/get-total-recipe")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(int), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetTotalRecipe()
    {
        var result = await _sender.Send(new AdminGetTotalRecipeNumberQuery
        {
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpGet("statistic/get-recipe-ranking-by-views")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<RankingStatisticEntity>), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetRecipeRankingByView()
    {
        var result = await _sender.Send(new AdminGetRankingRecipesStatisticQuery
        {
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("statistic/get-tag-ranking-by-popular")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<RankingStatisticEntity>), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetTagRankingByPopular([FromBody] AdminGetTagDTO adminGetTagDTO)
    {
        var result = await _sender.Send(new AdminGetRankingTagsStatisticQuery
        {
            Lang = adminGetTagDTO.Language
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }
}