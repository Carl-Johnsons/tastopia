using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostService.Application.Interactions.Commands;
using PostService.Application.Interactions.Queries;
using PostService.Domain.DTOs;

namespace PostService.API.Controllers;

[Route("api/post/interaction")]
[ApiController]
[Authorize]
public class InteractionsCotroller : BaseApiController
{
    public InteractionsCotroller(ISender sender, IHttpContextAccessor httpContextAccessor) : base(sender, httpContextAccessor)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateInteractionDTO createInteractionDTO)
    {
        var result = await _sender.Send(new CreateInteractionCommand
        {
            Value = createInteractionDTO.Value,
            Code = createInteractionDTO.Code,
            Gif = createInteractionDTO.Gif
        });

        result.ThrowIfFailure();

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteInteractionDTO deleteInteractionDTO)
    {
        var result = await _sender.Send(new DeleteInteractionCommand
        {
            Id = deleteInteractionDTO.Id
        });
        result.ThrowIfFailure();
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var post = await _sender.Send(new GetAllInteractionsQuery());
        post.ThrowIfFailure();
        return Ok(post.Value);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateInteractionDTO updateInteractionDTO)
    {
        var result = await _sender.Send(new UpdateInteractionCommand
        {
            Id = updateInteractionDTO.Id,
            Value = updateInteractionDTO.Value
        });
        result.ThrowIfFailure();
        return Ok();
    }
}
