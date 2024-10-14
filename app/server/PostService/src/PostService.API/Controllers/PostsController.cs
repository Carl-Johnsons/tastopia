using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostService.Application.Interactions.Commands;
using PostService.Application.Interactions.Queries;
using PostService.Application.Posts.Commands;
using PostService.Application.Posts.Queries;
using PostService.Application.Tags.Commands;
using PostService.Domain.DTOs;
using System.IdentityModel.Tokens.Jwt;

namespace PostService.API.Controllers;

[Route("api/post")]
[ApiController]
[Authorize]
public class PostsController : BaseApiController
{
    public PostsController(ISender sender, IHttpContextAccessor httpContextAccessor) : base(sender, httpContextAccessor)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreatePostDTO createPostDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var postResult = await _sender.Send(new CreatePostCommand
        {
            UserId = Guid.Parse(subjectId!),
            CreatePostDTO = createPostDTO
        });
        postResult.ThrowIfFailure();

        return Ok(postResult.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] Guid id)
    {
        var result = await _sender.Send(new GetPostByIdQuery
        {
            Id = id
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll([FromQuery] PaginatedPostListDTO paginatedPostListDTO)
    {
        var posts = await _sender.Send(new GetAllPostsQuery
        {
            Skip = paginatedPostListDTO.Skip
        });

        posts.ThrowIfFailure();
        return Ok(posts.Value);
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetByUserId([FromQuery] Guid userId)
    {
        var posts = await _sender.Send(new GetPostsByUserIdQuery
        {
            UserId = userId
        });

        posts.ThrowIfFailure();
        return Ok(posts.Value);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePostDTO updatePostDTO)
    {
        var result = await _sender.Send(new UpdatePostCommand
        {
            PostId = updatePostDTO.Id,
            Content = updatePostDTO.Content,
            Active = updatePostDTO.Active,
            TagIds = updatePostDTO.TagIds
        });

        result.ThrowIfFailure();

        if (result.IsSuccess)
        {
            await _sender.Send(new DeletePostTagCommand
            {
                PostId = updatePostDTO.Id
            });

            foreach (var t in updatePostDTO.TagIds!)
            {
                var postTag = await _sender.Send(new CreatePostTagCommand
                {
                    PostId = updatePostDTO.Id,
                    TagId = t
                });

                postTag.ThrowIfFailure();
            }
        }

        return Ok();
    }

    [HttpPost("interact")]
    public async Task<IActionResult> InteractPost([FromBody] CreatePostInteractionDTO createPostInteractionDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new CreatePostInteractionCommand
        {
            PostId = createPostInteractionDTO.PostId,
            InteractionId = createPostInteractionDTO.InteractionId,
            UserId = Guid.Parse(subjectId!)
        });

        result.ThrowIfFailure();

        return Ok();
    }

    [HttpPut("interact")]
    public async Task<IActionResult> UpdatePostInteraction([FromBody] CreatePostInteractionDTO createPostInteractionDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new UpdatePostInteractionCommand
        {
            PostId = createPostInteractionDTO.PostId,
            InteractionId = createPostInteractionDTO.InteractionId,
            UserId = Guid.Parse(subjectId!)
        });

        result.ThrowIfFailure();

        return Ok();
    }

    [HttpGet("interact")]
    public async Task<IActionResult> GetInteractionByPostId([FromQuery] Guid id)
    {
        var interactions = await _sender.Send(new GetInteractionByPostIdQuery
        {
            PostId = id
        });

        interactions.ThrowIfFailure();
        return Ok(interactions.Value);
    }

    [HttpGet("interact/user")]
    public async Task<IActionResult> GetInteractionByPostIdAndUserId([FromQuery] Guid id)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var interactions = await _sender.Send(new GetInteractionByPostIdQuery
        {
            PostId = id,
            UserId = Guid.Parse(subjectId!)
        });

        interactions.ThrowIfFailure();
        return Ok(interactions.Value);
    }

    [HttpPost("report")]
    public async Task<IActionResult> CreateReportPost([FromBody] CreatePostReport createPostReport)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var post = await _sender.Send(new CreatePostReportCommand
        {
            PostId = createPostReport.PostId,
            UserId = Guid.Parse(subjectId!),
            Reason = createPostReport.Reason
        });

        post.ThrowIfFailure();
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("report/all")]
    public async Task<IActionResult> GetAllReportPost([FromQuery] PaginatedReportListDTO dto)
    {
        var result = await _sender.Send(new GetListReportPostQuery
        {
            Skip = dto.Skip,
            Limit = dto.Limit
        });
        result.ThrowIfFailure();

        return Ok(result.Value);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("report")]
    public async Task<IActionResult> GetReportPost([FromQuery] Guid id)
    {
        var result = await _sender.Send(new GetPostReportByPostIdCommand
        {
            PostId = id
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpDelete("interact")]
    public async Task<IActionResult> UndoInteractPost([FromBody] UninteractionPostDTO uninteractionPostDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new UninteractionPostCommand
        {
            PostId = uninteractionPostDTO.PostId,
            UserId = Guid.Parse(subjectId!)
        });

        result.ThrowIfFailure();

        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> DeletePost([FromBody] DeletePostDTO dto)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var result = await _sender.Send(new DeletePostCommand
        {
            PostId = dto.Id,
            UserId = Guid.Parse(subjectId!)
        });

        result.ThrowIfFailure();

        return Ok();
    }
}
