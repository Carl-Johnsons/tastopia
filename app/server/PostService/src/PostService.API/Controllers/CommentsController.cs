using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostService.Application.Comments.Commands;
using PostService.Application.Comments.Queries;
using PostService.Domain.DTOs;
using System.IdentityModel.Tokens.Jwt;

namespace PostService.API.Controllers;

[Route("/api/post/comment")]
[ApiController]
[Authorize]
public class CommentsController : BaseApiController
{
    public CommentsController(ISender sender, IHttpContextAccessor httpContextAccessor) : base(sender, httpContextAccessor)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCommentDTO createCommentDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var comment = await _sender.Send(new CreateCommentCommand
        {
            Content = createCommentDTO.Content,
            UserId = Guid.Parse(subjectId!),
            PostId = createCommentDTO.PostId,
        });

        comment.ThrowIfFailure();

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetByPostId([FromQuery] PaginatedCommentListDTO paginatedCommentListDTO)
    {
        var result = await _sender.Send(new GetCommentByPostIdQuery
        {
            PostId = paginatedCommentListDTO.PostId,
            Skip = paginatedCommentListDTO.Skip
        });

        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCommentDTO updateCommentDTO)
    {
        var result = await _sender.Send(new UpdateCommetCommand
        {
            Id = updateCommentDTO.Id,
            Content = updateCommentDTO.Content
        });

        result.ThrowIfFailure();
        return Ok();
    }

    [HttpPost("reply")]
    public async Task<IActionResult> Reply([FromBody] CreateReplyCommentDTO createCommentDTO)
    {
        var claims = _httpContextAccessor.HttpContext?.User.Claims;
        var subjectId = claims?.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub)?.Value;

        var comment = await _sender.Send(new CreateCommentCommand
        {
            Content = createCommentDTO.Content,
            UserId = Guid.Parse(subjectId!),
            PostId = createCommentDTO.PostId,
        });

        comment.ThrowIfFailure();

        var reply = await _sender.Send(new CreateReplyCommand
        {
            CommentId = createCommentDTO.CommentId,
            ReplyCommentId = comment.Value.Id,
        });
        
        reply.ThrowIfFailure();

        return Ok();
    }
}
