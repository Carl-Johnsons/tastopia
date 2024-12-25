namespace DuendeIdentityServer.Controllers;

public class BaseApiController : ControllerBase
{
    protected readonly ISender _sender;
    protected readonly IMapper _mapper;
    protected readonly IHttpContextAccessor _httpContextAccessor;

    public BaseApiController(ISender sender, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _sender = sender;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }
}
