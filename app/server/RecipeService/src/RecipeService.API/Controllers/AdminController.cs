using AutoMapper;
using Contract.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using RecipeService.API.DTOs;
using RecipeService.Application.Activities;
using RecipeService.Application.Recipes.Queries;
using RecipeService.Application.Reports.Commands;
using RecipeService.Application.Reports.Queries;
using RecipeService.Application.Tags.Commands;
using RecipeService.Application.Tags.Queries;
using RecipeService.Domain.Entities;
using RecipeService.Domain.Responses;
using System.IdentityModel.Tokens.Jwt;
using static RecipeService.Application.Recipes.Queries.AdminGetNumberOfRecipesStatisticQueryHandler;

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
    [HttpGet("get-tags")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(PaginatedAdminTagListResponse), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> AdminGetTag([FromQuery] PaginatedDTO paginatedDTO)
    {
        var result = await _sender.Send(new AdminGetTagsQuery
        {
            PaginatedDTO = paginatedDTO
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("create-tag")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Tag), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> CreateTag([FromForm] CreateTagDTO createTagDTO)
    {
        var result = await _sender.Send(new CreateTagCommand
        {
            Code = createTagDTO.Code,
            Value = createTagDTO.Value,
            Category = createTagDTO.Category,
            TagImage = createTagDTO.TagImage
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("update-tag")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Tag), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> UpdateTag([FromForm] UpdateTagDTO updateTagDTO)
    {
        var result = await _sender.Send(new UpdateTagCommand
        {
            TagId = updateTagDTO.TagId,
            Code = updateTagDTO.Code,
            Value = updateTagDTO.Value,
            Category = updateTagDTO.Category,
            Status = updateTagDTO.Status,
            TagImage = updateTagDTO.TagImage
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("tag-detail")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Tag), 200)]
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

    [HttpPost("get-recipe-statistic")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<StatisticEntity>), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetRecipeStatistic([FromBody] AdminGetRecipeStatisticDTO adminGetRecipeStatisticDTO)
    {
        var result = await _sender.Send(new AdminGetNumberOfRecipesStatisticQuery
        {
            Language = adminGetRecipeStatisticDTO.Language,
            RangeType = adminGetRecipeStatisticDTO.RangeType,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpGet("get-total-recipe")]
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

    [HttpGet("get-recipe-ranking-by-views")]
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

    [HttpGet("get-tag-ranking-by-popular")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<RankingStatisticEntity>), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> GetTagRankingByPopular()
    {
        var result = await _sender.Send(new AdminGetRankingTagsStatisticQuery
        {
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }
}