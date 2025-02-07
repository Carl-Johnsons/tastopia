using NotificationService.Infrastructure.Persistence.Mockup.Data;
using Serilog;

namespace NotificationService.Infrastructure.Persistence.Mockup;

internal class MockupData
{
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    public MockupData(ApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task SeedAllData()
    {
        await SeedNotificationTemplate();
        await SeedNotifications();
    }
    private async Task SeedNotificationTemplate()
    {
        if (!_context.NotificationTemplates.Any())
        {
            Log.Information("Seed notification template");
            await _context.NotificationTemplates.AddRangeAsync(NotificationTemplateMockup.Data);
            await _unitOfWork.SaveChangeAsync();
        }
    }

    private async Task SeedNotifications()
    {
        if (!_context.Notifications.Any())
        {
            Log.Information("Seed notifications");
            await _context.Notifications.AddRangeAsync(NotificationMockup.GenerateRandomNotifications());
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
