using Contract.Utilities;
using Newtonsoft.Json;
using System;
using UserService.Domain.Entities;
using UserService.Infrastructure.Persistence.Mockup.Data;
namespace UserService.Infrastructure.Persistence.Mockup;

internal class MockupData
{
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly string SeedDataPath = EnvUtility.IsProduction() ? "seeds" : Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.Parent?.FullName!, "seeds") ?? "";

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
        await SeedUserReportDataAsync();
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
        var maleAvtUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/orvtiv8oxehgwbvmt403.png";
        var femaleAvtUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024620/default_storage/hud0frffejraoexs28ol.png";
        var backgroundUrl = "https://res.cloudinary.com/dhphzuojz/image/upload/v1735024288/default_storage/nuyo1txfw4qontqlcca1.png";

        var seedUserFile = File.ReadAllText(Path.Combine(SeedDataPath, "accounts.json"));
        var seedAllUsers = JsonConvert.DeserializeObject<List<SeedUser>>(seedUserFile) ?? [];

        var seedWrongUserFile = File.ReadAllText(Path.Combine(SeedDataPath, "wrong-users.json"));
        var seedWrongUsers = JsonConvert.DeserializeObject<List<SeedUser>>(seedWrongUserFile) ?? [];

        var seedAddressFile = File.ReadAllText(Path.Combine(SeedDataPath, "addresses.json"));
        var seedAddresses = JsonConvert.DeserializeObject<List<string>>(seedAddressFile) ?? [];

        var seedBioFile = File.ReadAllText(Path.Combine(SeedDataPath, "bios.json"));
        var seedBios = JsonConvert.DeserializeObject<List<string>>(seedBioFile) ?? [];

        var seedToxicBioFile = File.ReadAllText(Path.Combine(SeedDataPath, "toxic-bios.json"));
        var seedToxicBios = JsonConvert.DeserializeObject<List<string>>(seedToxicBioFile) ?? [];

        var seedUsers = seedAllUsers.Where(u => u.RoleCode == "USER").ToList();
        var seedAdminUsers = seedAllUsers.Where(u => u.RoleCode != "USER").ToList();

        Random random = new Random();
        var users = seedUsers.Select(u => new User {
            AccountId = Guid.Parse(u.Id),
            AccountUsername = u.UserName,
            DisplayName = u.DisplayName,
            IsAdmin = false,
            IsAccountActive = true,
            Gender = u.Gender,
            AvatarUrl = u.Gender == "Male" ? maleAvtUrl : femaleAvtUrl,
            BackgroundUrl = backgroundUrl,
            Address = seedAddresses[random.Next(seedAddresses.Count)],
            Bio = seedBios[random.Next(seedBios.Count)],
            TotalRecipe = random.Next(6, 8)
        });

        var wrongUsers = seedWrongUsers.Select(u => new User
        {
            AccountId = Guid.Parse(u.Id),
            AccountUsername = u.UserName,
            DisplayName = u.DisplayName,
            IsAdmin = false,
            IsAccountActive = true,
            Gender = u.Gender,
            AvatarUrl = u.Gender == "Male" ? maleAvtUrl : femaleAvtUrl,
            BackgroundUrl = backgroundUrl,
            Address = seedAddresses[random.Next(seedAddresses.Count)],
            Bio = seedToxicBios[random.Next(seedToxicBios.Count)],
            TotalRecipe = 0
        });
        var adminUsers = seedAdminUsers.Select(u => new User
        {
            AccountId = Guid.Parse(u.Id),
            AccountUsername = u.UserName,
            DisplayName = u.DisplayName,
            IsAdmin = true,
            IsAccountActive = true,
            AvatarUrl = u.Gender == "Male" ? maleAvtUrl : femaleAvtUrl,
            BackgroundUrl = backgroundUrl,
            Address = seedAddresses[random.Next(seedAddresses.Count)],
            Dob = GenerateRandomBirthDate(random)
        });
        _context.Users.AddRange(users);
        _context.Users.AddRange(wrongUsers);
        _context.Users.AddRange(adminUsers);
        await _context.SaveChangesAsync();
    }
   

    private async Task SeedUserFollowDataAsync()
    {
        if (_context.UserFollows.Any())
        {
            return;
        }
        var seedUserFile = File.ReadAllText(Path.Combine(SeedDataPath, "accounts.json"));
        var seedUsers = JsonConvert.DeserializeObject<List<SeedUser>>(seedUserFile) ?? [];
        var userIds = seedUsers.Where(u => u.RoleCode == "USER").Select(u => Guid.Parse(u.Id)).ToHashSet();
        var users = _context.Users.Where(u => userIds.Contains(u.AccountId)).ToList();
        Random random = new Random();
        foreach ( var user in users ) {
            int numberOfFollowing = random.Next(25);
            user.TotalFollowing = numberOfFollowing;
            var allUsers = users.Where(u => u.AccountId != user.AccountId).ToList();
            var randomUsers = allUsers.OrderBy(u => random.Next()).Take(numberOfFollowing).ToList();
            foreach(var u in randomUsers )
            {
                u.TotalFollower = (u.TotalFollower ?? 0) + 1;
                var follow = new UserFollow
                {
                    FollowerId = user.AccountId,
                    FollowingId = u.AccountId,
                };
                _context.Users.Update(u);
                _context.UserFollows.Add(follow);
            }
            _context.Users.Update(user);

        }
        await _unitOfWork.SaveChangeAsync();
    }

    private async Task SeedUserReportDataAsync()
    {
        if (_context.UserReports.Any())
        {
            return;
        }
        var seedUserFile = File.ReadAllText(Path.Combine(SeedDataPath, "accounts.json"));
        var seedUsers = JsonConvert.DeserializeObject<List<SeedUser>>(seedUserFile) ?? [];

        var seedWrongUserFile = File.ReadAllText(Path.Combine(SeedDataPath, "wrong-users.json"));
        var seedWrongUsers = JsonConvert.DeserializeObject<List<SeedWrongUser>>(seedWrongUserFile) ?? [];

        var userIds = seedUsers.Where(u => u.RoleCode == "USER").Select(u => Guid.Parse(u.Id)).ToHashSet();
        var users = _context.Users.Where(u => userIds.Contains(u.AccountId)).ToList();
        Random random = new Random();
        foreach (var user in seedWrongUsers)
        {
            var allUsers = users.ToList();
            var randomUsers = allUsers.OrderBy(u => random.Next()).Take(25).ToList();
            foreach (var u in randomUsers)
            {
                var report = new UserReport
                {
                    Id = Guid.NewGuid(),
                    ReporterId = u.AccountId,
                    ReportedId = Guid.Parse(user.Id),
                    ReasonCodes = user.ReasonCodes,
                    AdditionalDetails = user.AdditionalDetails,
                };
                _context.UserReports.Add(report);
            }
        }
        await _unitOfWork.SaveChangeAsync();
    }

    private class SeedUser
    {
        public string Id { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string RoleCode { get; set; } = null!;
        public string Gender { get; set; } = null!;
    }

    private class SeedWrongUser
    {
        public string Id { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string RoleCode { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public List<string> ReasonCodes { get; set; } = [];
        public string AdditionalDetails { get; set; } = null!;
    }

    private static DateTime GenerateRandomBirthDate(Random random)
    {
        DateTime today = DateTime.Today;
        DateTime maxDate = today.AddYears(-19);
        DateTime minDate = today.AddYears(-30);
        int range = (maxDate - minDate).Days;
        return minDate.AddDays(random.Next(range + 1)).ToUniversalTime();
    }
}
