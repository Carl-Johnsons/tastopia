using Microsoft.AspNetCore.Authorization;

namespace NotificationService.API.Controllers;

[Route("api/notification")]
[ApiController]
[Authorize]
public partial class NotificationController : BaseApiController
{
    public NotificationController(
            ISender sender,
            IHttpContextAccessor httpContextAccessor
        ) : base(sender, httpContextAccessor)
    {
    }

}
