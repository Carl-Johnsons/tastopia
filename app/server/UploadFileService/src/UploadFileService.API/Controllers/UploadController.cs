using Microsoft.AspNetCore.Mvc;
using UploadFileService.Application.CloudinaryFiles.Commands;
using UploadFileService.Application.CloudinaryFiles.Queries;

namespace UploadFileService.API.Controllers;


[Route("api/upload")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly ISender _sender;

    public UploadController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _sender.Send(new GetAllCloudinaryFilesQuery());
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetCloudinaryFileByIdDTO getCloudinaryFileByIdDTO)
    {
        if(getCloudinaryFileByIdDTO.Id != null)
        {
            var result = await _sender.Send(new GetCloudinaryFileByIdQuery 
            { 
                Id  = getCloudinaryFileByIdDTO.Id
            });
            result.ThrowIfFailure();
            return Ok(result.Value);
        }
        return BadRequest("Get file fail");
    }

    [HttpPost("image")]
    public async Task<IActionResult> UploadImage([FromForm] UploadFileDTO uploadFileDTO)
    {
        var result = await _sender.Send(new CreateCloudinaryImageFileCommand
        {
            FormFile = uploadFileDTO.File
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("video")]
    public async Task<IActionResult> UploadVideo([FromForm] UploadFileDTO uploadFileDTO)
    {
        var result = await _sender.Send(new CreateCloudinaryVideoFileCommand
        {
            FormFile = uploadFileDTO.File
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpPost("raw")]
    public async Task<IActionResult> UploadRaw([FromForm] UploadFileDTO uploadFileDTO)
    {
        var result = await _sender.Send(new CreateCloudinaryRawFileCommand
        {
            FormFile = uploadFileDTO.File
        });
        result.ThrowIfFailure();
        return Ok(result.Value);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] DeleteCloudinaryFileByIdDTO deleteCloudinaryFileById)
    {
        if(deleteCloudinaryFileById.Id != null) {
        
            var result = await _sender.Send(new DeleteCloudinaryFileCommand
            {
                Id = deleteCloudinaryFileById.Id
            });
            result?.ThrowIfFailure();
            return NoContent();
        }
        return BadRequest("Delete fail");
    }
}
