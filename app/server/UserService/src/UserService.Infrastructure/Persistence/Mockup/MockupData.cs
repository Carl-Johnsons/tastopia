using UserService.Infrastructure.Persistence.Mockup.Data;

namespace UserService.Infrastructure.Persistence.Mockup;

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
        await SeedSettingDataAsync();
        await SeedUserDataAsync();
        await SeedUserFollowDataAsync();
    }

    private async Task SeedSettingDataAsync()
    {
        if (_context.Settings.Any())
        {
            return;
        }

        _context.Settings.AddRange(SettingData.Data);
        await _unitOfWork.SaveChangeAsync();
    }

    private async Task SeedUserDataAsync()
    {
        if (_context.Users.Any())
        {
            return;
        }

        _context.Users.AddRange(UserData.Data);
        await _unitOfWork.SaveChangeAsync();
    }

    private async Task SeedUserFollowDataAsync()
    {
        if (_context.UserFollows.Any())
        {
            return;
        }

        _context.UserFollows.AddRange(UserData.UserFollowData);
        await _unitOfWork.SaveChangeAsync();
    }
}
