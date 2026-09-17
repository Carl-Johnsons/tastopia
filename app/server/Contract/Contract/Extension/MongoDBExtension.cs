using System.Collections.Concurrent;
using Contract.Utilities;
using MongoDB.Driver;
using MongoDB.Driver.Core.Extensions.DiagnosticSources;

namespace Contract.Extension;

public static class MongoDBExtension
{
    private static readonly ConcurrentDictionary<string, MongoClient> _clients = new();

    public static MongoClient CreateTracedMongoClient(string? connectionString = null)
    {
        return CreateTracedMongoClient(connectionString, null);
    }

    public static MongoClient CreateTracedMongoClient(
        string? connectionString,
        InstrumentationOptions? options)
    {
        var connStr = connectionString ?? EnvUtility.GetMongoDBConnectionString();
        return _clients.GetOrAdd(connStr, key =>
        {
            var setting = MongoClientSettings.FromConnectionString(key);
            var instrumentationOptions = options ?? new InstrumentationOptions
            {
                CaptureCommandText = true
            };
            setting.ClusterConfigurator = cb => cb
                .Subscribe(new DiagnosticsActivityEventSubscriber(instrumentationOptions));
            return new MongoClient(setting);
        });
    }
}


