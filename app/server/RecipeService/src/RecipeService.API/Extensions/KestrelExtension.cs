using Contract.Utilities;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using RecipeService.API.Extensions;

namespace RecipeService.API.Extensions;

public static class KestrelExtension
{
    public static WebApplicationBuilder ConfigureKestrel(this WebApplicationBuilder builder)
    {
        EnvUtility.LoadEnvFile();

        var httpPort = DotNetEnv.Env.GetInt("PORT", 0);
        var httpsPort = DotNetEnv.Env.GetInt("HTTPS_PORT", 0);
        var certPath = DotNetEnv.Env.GetString("ASPNETCORE_Kestrel__Certificates__Default__Path");
        var certPassword = DotNetEnv.Env.GetString("ASPNETCORE_Kestrel__Certificates__Default__Password");
        Console.WriteLine("++++++++++++++++++++");
        Console.WriteLine(httpPort);
        Console.WriteLine(httpsPort);
        Console.WriteLine(certPath);
        Console.WriteLine(certPassword);
        Console.WriteLine("++++++++++++++++++++");

        builder.WebHost.ConfigureKestrel(options =>
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
