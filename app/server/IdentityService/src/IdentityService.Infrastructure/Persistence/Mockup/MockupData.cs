using IdentityService.Infrastructure.Persistence.Mockup.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace IdentityService.Infrastructure.Persistence.Mockup;

internal class MockupData
{
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
        await SeedSuperAdminRoleAsync();
        await SeedUserDataAsync();
    }


    public async Task SeedSuperAdminRoleAsync()
    {
        // Create a default admin user
        var adminUser = new ApplicationAccount
        {
            Id = "f9a8c16e-610a-49f5-aac0-82183d8c3a16",
            UserName = "admin",
            Email = "admin@example.com",
            EmailConfirmed = true,
            PhoneNumber = "0111111119",
            PhoneNumberConfirmed = true,
            /* Custom attribute */
            IsActive = true,
        };

        string userPassword = "Pass123$";
        var user = await _userManager.FindByEmailAsync("admin@example.com");

        if (user == null)
        {
            _logger.LogInformation("***\tSeed super admin data\t***");
            var createAdminUser = await _userManager.CreateAsync(adminUser, userPassword);
            if (createAdminUser.Succeeded)
            {
                await _userManager.AddToRoleAsync(adminUser, "SUPER ADMIN");
                _logger.LogInformation("***\tSeed admin data successfully\t***");
            }
        }
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
        foreach (var user in ApplicationAccountData.Data)
        {
            var userResult = _userManager.FindByNameAsync(user.UserName!).Result;
            if (userResult == null)
            {
                var result = _userManager.CreateAsync(user, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                await _userManager.AddToRoleAsync(user, "User");

                _logger.LogInformation($"{user.UserName} created");
            }
        }
    }
}
