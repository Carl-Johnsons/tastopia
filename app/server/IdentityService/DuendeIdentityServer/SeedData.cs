using DuendeIdentityServer.Data;
using DuendeIdentityServer.Data.Mockup;
using DuendeIdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DuendeIdentityServer;

public class SeedData
{
    public static async void EnsureSeedData(string connectionString)
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddDbContext<ApplicationDbContext>();

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        using var serviceProvider = services.BuildServiceProvider();

        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var context = scope.ServiceProvider.GetService<ApplicationDbContext>()!;
        context.Database.Migrate();

        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var mockUpData = new MockupData(userMgr, context);


        await SeedUserRoleAsync(serviceProvider);
        mockUpData.SeedUserData();
    }

    private static async Task SeedUserRoleAsync(ServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string[] roleNames = { "Admin", "User" };
        IdentityResult roleResult;

        await Console.Out.WriteLineAsync("================================================");
        await Console.Out.WriteLineAsync("Seed role data");
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                await Console.Out.WriteLineAsync($"Seed role '{roleName}' data successfully");
            }
        }
        await Console.Out.WriteLineAsync("================================================");

        // Create a default admin user
        var adminUser = new ApplicationUser
        {
            Id = "f9a8c16e-610a-49f5-aac0-82183d8c3a16",
            UserName = "admin",
            Email = "admin@example.com",
            EmailConfirmed = true,
            PhoneNumber = "0111111119",
            PhoneNumberConfirmed = true,
            /* Custom attribute */
            Name = "Admin",
            Gender = "Nam",
            AvatarUrl = "https://scontent.fvca1-3.fna.fbcdn.net/v/t1.6435-9/84749807_107364874171943_5917239107272048640_n.jpg?_nc_cat=110&ccb=1-7&_nc_sid=5f2048&_nc_ohc=lX3TOXeqiYkQ7kNvgFEP5Uw&_nc_ht=scontent.fvca1-3.fna&oh=00_AYCnvLsQZfP1Q4TnefnX9fHZY4xCyZBZVVQNsi47CMrEcg&oe=669E447E",
            BackgroundUrl = "https://scontent.fvca1-3.fna.fbcdn.net/v/t1.6435-9/84749807_107364874171943_5917239107272048640_n.jpg?_nc_cat=110&ccb=1-7&_nc_sid=5f2048&_nc_ohc=lX3TOXeqiYkQ7kNvgFEP5Uw&_nc_ht=scontent.fvca1-3.fna&oh=00_AYCnvLsQZfP1Q4TnefnX9fHZY4xCyZBZVVQNsi47CMrEcg&oe=669E447E",
            Active = true,
            Dob = new DateTime(2005, 12, 3)
        };

        string userPassword = "Pass123$";
        var user = await userManager.FindByEmailAsync("admin@example.com");

        if (user == null)
        {
            await Console.Out.WriteLineAsync("================================================");
            await Console.Out.WriteLineAsync("Seed admin data");
            var createAdminUser = await userManager.CreateAsync(adminUser, userPassword);
            if (createAdminUser.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
                await Console.Out.WriteLineAsync("Seed admin data successfully");
            }
            await Console.Out.WriteLineAsync("================================================");

        }

    }

}
