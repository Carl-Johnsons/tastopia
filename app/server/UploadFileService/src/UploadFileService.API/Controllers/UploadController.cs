using Microsoft.AspNetCore.Mvc;
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


    
}
