using Contract.Constants;
using UserService.Domain.Interfaces;
using UserService.Infrastructure;

namespace UserService.API;

public static class AppCommandHandler
{
    public static async Task<bool> TryHandleAsync(this WebApplicationBuilder builder, string[] args)
    {
        var uniqueArgs = args
            .Where(COMMAND_ARGS.All.Contains)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();
        if (uniqueArgs.Length == 0)
        {
            return false;
        }

        builder.Services.AddMinimalInfrastructureServices();
        var minimalApp = builder.Build();
        using var scope = minimalApp.Services.CreateScope();
        var sp = scope.ServiceProvider;
        var dbContext = sp.GetRequiredService<IApplicationDbContext>();
        foreach (var arg in uniqueArgs)
        {
            switch (arg)
            {
                case COMMAND_ARGS.MIGRATE:
                    dbContext.MigrateDb(sp);
                    break;
                case COMMAND_ARGS.SEED:
                    dbContext.SeedDb(sp).GetAwaiter().GetResult();
                    break;
            }
        }
        return true;
    }
}