using DuendeIdentityServer.Data.Mockup.Data;
using DuendeIdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace DuendeIdentityServer.Data.Mockup;

public class MockupData
{
    public UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    public MockupData(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public void SeedUserData()
    {
        Log.Debug("Seeding user data");

        foreach (var user in ApplicationUserData.Data)
        {
            var userResult = _userManager.FindByNameAsync(user.UserName!).Result;
            if (userResult == null)
            {
                var result = _userManager.CreateAsync(user, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Log.Debug($"{user.UserName} created");
            }
            Log.Debug($"{user.UserName} already exists");
        }
    }
}

