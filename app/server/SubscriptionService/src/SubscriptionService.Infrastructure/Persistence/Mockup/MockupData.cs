using SubscriptionService.Infrastructure.Persistence.Mockup.Data;

namespace SubscriptionService.Infrastructure.Persistence.Mockup;

internal class MockupData
{
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    public MockupData(ApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task SeedDataAsync()
    {
        await SeedEventDataAsync();
    }

    public async Task SeedEventDataAsync()
    {
        if (!_context.Events.Any())
        {
            _context.Events.AddRange(EventData.Data);
            await _unitOfWork.SaveChangeAsync();
        }
    }

}
