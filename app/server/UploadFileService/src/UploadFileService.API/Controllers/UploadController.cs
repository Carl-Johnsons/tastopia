using Contract.DTOs.UploadFileDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UploadFileService.API.DTOs;
using UploadFileService.Application.Files.Commands;
namespace UploadFileService.API.Controllers;


[Route("api/upload")]
[ApiController]
[Authorize]
public class UploadController : ControllerBase
{
    private readonly ISender _sender;

    public UploadController(ISender sender)
    {
        _sender = sender;
    }

    [AllowAnonymous]
    [HttpPost("upload-images")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ListFileDTO), 200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> UploadMultipleImage([FromForm] UploadMultipleImageFileDTO uploadMultipleImageFileDTO)
    {

        var result = await _sender.Send(new CreateMultipleImageFileCommand
        {
            FormFiles = uploadMultipleImageFileDTO.Files,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpPost("delete-images")]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> DeleteMultipleImage([FromForm] DeleteMultipleImageFileDTO deleteMultipleImageFileDTO)
    {

        var result = await _sender.Send(new DeleteMultipleImageFileCommand
        {
            DeleteUrls = deleteMultipleImageFileDTO.DeleteUrls,
        });
        result.ThrowIfFailure();
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("update-images")]
    [Produces("application/json")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(ErrorResponseDTO), 400)]
    public async Task<IActionResult> UpdateMultipleImage([FromForm] UpdateMultipleImageFileDTO updateMultipleImageFileDTO)
    {

        var result = await _sender.Send(new UpdateMultipleImageFileCommand
        {
            FormFiles = updateMultipleImageFileDTO.Files,
            DeleteUrls = updateMultipleImageFileDTO.DeleteUrls,
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }



}
