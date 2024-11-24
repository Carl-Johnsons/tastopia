using IdentityService.Infrastructure.Persistence.Mockup.Data;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Infrastructure.Persistence.Mockup;

internal class MockupData
{
    private UserManager<ApplicationAccount> _userManager;
    private RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    public MockupData(ApplicationDbContext context, IUnitOfWork unitOfWork, UserManager<ApplicationAccount> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedUserRoleAsync()
    {
        string[] roleNames = { "Admin", "User" };
        IdentityResult roleResult;

        await Console.Out.WriteLineAsync("================================================");
        await Console.Out.WriteLineAsync("Seed role data");
        foreach (var roleName in roleNames)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                await Console.Out.WriteLineAsync($"Seed role '{roleName}' data successfully");
            }
        }
        await Console.Out.WriteLineAsync("================================================");

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
            Active = true,
        };

        string userPassword = "Pass123$";
        var user = await _userManager.FindByEmailAsync("admin@example.com");

        if (user == null)
        {
            await Console.Out.WriteLineAsync("================================================");
            await Console.Out.WriteLineAsync("Seed admin data");
            var createAdminUser = await _userManager.CreateAsync(adminUser, userPassword);
            if (createAdminUser.Succeeded)
            {
                await _userManager.AddToRoleAsync(adminUser, "Admin");
                await Console.Out.WriteLineAsync("Seed admin data successfully");
            }
            await Console.Out.WriteLineAsync("================================================");
        }
    }


    public async Task SeedUserData()
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

                Console.WriteLine($"{user.UserName} created");
            }
            Console.WriteLine($"{user.UserName} already exists");
        }
    }


}
