using Contract.Utilities;
using IdentityService.Infrastructure.Persistence.Mockup.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IdentityService.Infrastructure.Persistence.Mockup;

internal class MockupData
{
    private readonly string SeedDataPath = EnvUtility.IsProduction() ? "seeds" : Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.Parent?.FullName!, "seeds") ?? "";
    private UserManager<ApplicationAccount> _userManager;
    private RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<MockupData> _logger;
    public MockupData(ApplicationDbContext context, IUnitOfWork unitOfWork, UserManager<ApplicationAccount> userManager, RoleManager<IdentityRole> roleManager, ILogger<MockupData> logger)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    public async Task SeedAllData()
    {
        await SeedRoleGroupPermission();
        await SeedUserDataAsync();
    }

    public async Task SeedRoleGroupPermission()
    {
        _logger.LogInformation("***\tSeed role data\t***");
        foreach (var roleName in RoleGroupPermissionData.ROLES_DATA)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
                _logger.LogInformation($"***\tSeed role '{roleName}' data successfully\t***");
            }
        }

        if (!_context.Permissions.Any())
        {
            _logger.LogInformation("***\tSeed permission data\t***");
            await _context.Permissions.AddRangeAsync(RoleGroupPermissionData.PERMISSIONS_DATA);
        }

        if (!_context.Groups.Any())
        {
            _logger.LogInformation("***\tSeed group permission data\t***");
            await _context.Groups.AddRangeAsync(RoleGroupPermissionData.GROUPS_DATA);
        }
    }

    public async Task SeedUserDataAsync()
    {
        var seedAccountFile = File.ReadAllText(Path.Combine(SeedDataPath, "accounts.json"));
        var seedAccounts = JsonConvert.DeserializeObject<List<SeedAccount>>(seedAccountFile) ?? [];

        var seedWrongUserFile = File.ReadAllText(Path.Combine(SeedDataPath, "wrong-users.json"));
        var seedWrongUsers = JsonConvert.DeserializeObject<List<SeedAccount>>(seedWrongUserFile) ?? [];

        foreach (var seedAccount in seedAccounts)
        {
            if (!Enum.IsDefined(typeof(Contract.Constants.Roles.Code), seedAccount.RoleCode))
            {
                _logger.LogError($"Malicious seed account data with role {seedAccount.RoleCode}");
                continue;
            }

            var time = GetRandomDateTime().ToUniversalTime();
            var account = new ApplicationAccount
            {
                Id = seedAccount.Id,
                UserName = seedAccount.Username,
                Email = seedAccount.Email,
                PhoneNumber = seedAccount.PhoneNumber,

                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true,
                CreatedAt = time,
                UpdatedAt = time
            };

            var userResult = _userManager.FindByNameAsync(account.UserName).Result;
            if (userResult == null)
            {
                var result = _userManager.CreateAsync(account, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                await _userManager.AddToRoleAsync(account, seedAccount.RoleCode.ToUpper());

                _logger.LogInformation($"{account.UserName} created");
            }
        }

        foreach (var seedWrongUser in seedWrongUsers)
        {
            if (!Enum.IsDefined(typeof(Contract.Constants.Roles.Code), seedWrongUser.RoleCode))
            {
                _logger.LogError($"Malicious seed account data with role {seedWrongUser.RoleCode}");
                continue;
            }

            var time = GetRandomDateTime().ToUniversalTime();
            var account = new ApplicationAccount
            {
                Id = seedWrongUser.Id,
                UserName = seedWrongUser.Username,
                Email = seedWrongUser.Email,
                PhoneNumber = seedWrongUser.PhoneNumber,

                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                IsActive = true,
                CreatedAt = time,
                UpdatedAt = time
            };

            var userResult = _userManager.FindByNameAsync(account.UserName).Result;
            if (userResult == null)
            {
                var result = _userManager.CreateAsync(account, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                await _userManager.AddToRoleAsync(account, seedWrongUser.RoleCode);

                _logger.LogInformation($"{account.UserName} created");
            }
        }
    }

    //random datetime from today to last 365 days
    private DateTime GetRandomDateTime()
    {
        Random random = new Random();
        DateTime today = DateTime.Today;
        int daysRange = 365;
        int randomDays = random.Next(0, daysRange + 1);
        return today.AddDays(-randomDays);
    }

    private class SeedAccount
    {
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string RoleCode { get; set; } = null!;
    }
}
