using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostService.Application.Tags.Commands;
using PostService.Application.Tags.Queries;
using PostService.Domain.DTOs;

namespace PostService.API.Controllers;

[Route("api/post/tag")]
[ApiController]
[Authorize]
public class TagsController : BaseApiController
{
    public TagsController(ISender sender, IHttpContextAccessor httpContextAccessor) : base(sender, httpContextAccessor)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTagDTO createTagDTO)
    {
        var result = await _sender.Send(new CreateTagCommand
        {
            Value = createTagDTO.Value
        });

        result.ThrowIfFailure();
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tags = await _sender.Send(new GetAllTagsQuery());

        return Ok(tags.Value);

    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTagDTO updateTagDTO)
    {
        var tag = await _sender.Send(new UpdateTagCommand
        {
            TagId = updateTagDTO.Id,
            Value = updateTagDTO.Value
        });
        tag.ThrowIfFailure();
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteTagDTO deleteTagDTO)
    {
        var result = await _sender.Send(new DeleteTagCommand
        {
            TagId = deleteTagDTO.Id
        });

        result.ThrowIfFailure();
        return Ok();
    }

}
