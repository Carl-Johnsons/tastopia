using Contract.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace RecipeWorker.Extensions;

public static class KestrelExtension
{
    public static IWebHostBuilder ConfigureKestrel(this IWebHostBuilder builder)
    {
        EnvUtility.LoadEnvFile();

        var httpPort = DotNetEnv.Env.GetInt("WORKER_PORT", 0);
        var httpsPort = DotNetEnv.Env.GetInt("WORKER_HTTPS_PORT", 0);
        var certPath = DotNetEnv.Env.GetString("ASPNETCORE_Kestrel__Certificates__Default__Path");
        var certPassword = DotNetEnv.Env.GetString("ASPNETCORE_Kestrel__Certificates__Default__Password");
        Console.WriteLine("****************************************");
        Console.WriteLine(httpPort);
        Console.WriteLine(httpsPort);
        Console.WriteLine(certPath);
        Console.WriteLine(certPassword);
        Console.WriteLine("****************************************");

        builder.UseKestrel(options =>
        {
            options.ListenAnyIP(httpPort, listenOption =>
            {
                listenOption.Protocols = HttpProtocols.Http1;
            });

            options.ListenAnyIP(httpsPort, listenOption =>
            {
                listenOption.Protocols = HttpProtocols.Http1AndHttp2;
                // Can't use directly from dotnetenv, have to assign to an variable. Weird bug
                listenOption.UseHttps(certPath, certPassword);
            });
        });

        return builder;
    }
}
