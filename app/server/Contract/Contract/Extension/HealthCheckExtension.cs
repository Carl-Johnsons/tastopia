using Contract.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Bson;
using MongoDB.Driver;
using Npgsql;
using StackExchange.Redis;

namespace Contract.Extension;

public static class HealthCheckExtension
{
    public static WebApplicationBuilder ConfigureLivenessCheck(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
          .AddCheck(
            "self",
            () => HealthCheckResult.Healthy()

            , tags: ["live"]);
        return builder;
    }

    public static WebApplicationBuilder ConfigureMongoDBHealthCheck(this WebApplicationBuilder builder, string databaseName, string? connectionString = null)
    {
        var connStr = connectionString ?? EnvUtility.GetMongoDBConnectionString();
        var client = new MongoClient(connStr);
        var db = client.GetDatabase(databaseName);

        builder.Services.AddHealthChecks().AddAsyncCheck(
            $"mongodb-{databaseName}",
            async () =>
            {
                try
                {
                    await db.RunCommandAsync((Command<BsonDocument>)"{ping:1}");
                    return HealthCheckResult.Healthy($"MongoDB '{databaseName}' is healthy");
                }
                catch (Exception ex)
                {
                    return HealthCheckResult.Unhealthy($"MongoDB '{databaseName}' is unreachable: {ex.Message}");
                }
            },
            tags: ["ready"]
            );

        return builder;
    }

    public static WebApplicationBuilder ConfigurePostgresHealthCheck(this WebApplicationBuilder builder, string databaseName, string? connectionString = null)
    {
        var connStr = connectionString ?? EnvUtility.GetConnectionString();

        builder.Services.AddHealthChecks().AddAsyncCheck(
            $"postgres-{databaseName}",
            async () =>
            {
                try
                {
                    await using var conn = new NpgsqlConnection(connStr);
                    await conn.OpenAsync();

                    await using var cmd = new NpgsqlCommand("SELECT 1;", conn);
                    await cmd.ExecuteScalarAsync();

                    return HealthCheckResult.Healthy($"Postgres '{databaseName}' is healthy");
                }
                catch (Exception ex)
                {
                    return HealthCheckResult.Unhealthy($"Postgres '{databaseName}' is unreachable: {ex.Message}");
                }
            },
            tags: ["ready"]
            );

        return builder;
    }

    public static WebApplicationBuilder ConfigureRedisHealthCheck(this WebApplicationBuilder builder, string? connectionString = null)
    {
        var connStr = connectionString ?? EnvUtility.GetRedisConnectionString();
        ConnectionMultiplexer? connection = null;

        builder.Services.AddHealthChecks().AddAsyncCheck(
            "redis",
            async () =>
            {
                try
                {
                    if (connection == null || !connection.IsConnected)
                    {
                        try
                        {
                            connection?.Dispose();
                        }
                        catch
                        {
                        }

                        connection = await ConnectionMultiplexer.ConnectAsync(connStr);
                    }

                    if (!connection.IsConnected)
                    {
                        return HealthCheckResult.Unhealthy("Redis is not connected");
                    }

                    var db = connection.GetDatabase();
                    var latency = await db.PingAsync();
                    return HealthCheckResult.Healthy($"Redis is healthy (latency: {latency.TotalMilliseconds}ms)");
                }
                catch (Exception ex)
                {
                    connection = null;
                    return HealthCheckResult.Unhealthy($"Redis is unreachable: {ex.Message}");
                }
            },
            tags: ["ready"]
            );

        return builder;
    }

    /**
     * <summary>
     *  In order to use the health check UseHealthCheck after UseRouting
     * </summary>
     */
    public static WebApplication UseCustomHealthCheck(this WebApplication app)
    {
        app.MapHealthChecks("/health/live", new HealthCheckOptions
        {
            Predicate = check => check.Tags.Contains("live")
        });

        app.MapHealthChecks("/health/ready", new HealthCheckOptions
        {
            Predicate = check => check.Tags.Overlaps(["live", "ready", "masstransit"])
        });

        return app;
    }
}
