using Microsoft.Extensions.Logging;
using NotificationService.Infrastructure.Persistence.Mockup.Data;

namespace NotificationService.Infrastructure.Persistence.Mockup;

internal class MockupData
{
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<MockupData> _logger;

    public MockupData(ApplicationDbContext context, IUnitOfWork unitOfWork, ILogger<MockupData> logger)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task SeedAllDataAsync()
    {
        await SeedNotificationTemplate();
        //await SeedNotifications();
    }
    private async Task SeedNotificationTemplate()
    {
        if (!_context.NotificationTemplates.Any())
        {
            _logger.LogInformation("Seed notification template");
            await _context.NotificationTemplates.AddRangeAsync(NotificationTemplateMockup.Data);
            await _unitOfWork.SaveChangeAsync();
        }
    }

    private async Task SeedNotifications()
    {
        if (!_context.Notifications.Any())
        {
            _logger.LogInformation("Seed notifications");
            await _context.Notifications.AddRangeAsync(NotificationMockup.GenerateRandomNotifications());
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
