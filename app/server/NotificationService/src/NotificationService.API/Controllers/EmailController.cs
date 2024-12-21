using NotificationService.API.DTOs;
using NotificationService.Infrastructure.Services;

namespace NotificationService.API.Controllers;

[Route("api/email")]
[ApiController]
public partial class EmailController : BaseApiController
{
    private readonly IEmailService _emailService;
    public EmailController(
            ISender sender,
            IHttpContextAccessor httpContextAccessor,
            IEmailService emailService) : base(sender, httpContextAccessor)
    {
        _emailService = emailService;
    }

    [HttpPost]
    public async Task<IActionResult> SendEmail([FromBody] SendEmailDTO dto)
    {
        await _emailService.SendEmail(dto.EmailTo, dto.Subject, dto.Body);
        return NoContent();
    }

}
