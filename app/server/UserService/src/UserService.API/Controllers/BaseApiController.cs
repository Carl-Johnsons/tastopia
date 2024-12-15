using Microsoft.AspNetCore.Mvc;

namespace UserService.API.Controllers;

public class BaseApiController : ControllerBase
{
    protected readonly ISender _sender;
    protected readonly IHttpContextAccessor _httpContextAccessor;

    public BaseApiController(ISender sender, IHttpContextAccessor httpContextAccessor)
    {
        _sender = sender;
        _httpContextAccessor = httpContextAccessor;
    }
}
