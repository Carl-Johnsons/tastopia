using Contract.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Security.Cryptography.X509Certificates;

namespace RecipeWorker.Extensions;

public static class KestrelExtension
{
    public static IWebHostBuilder ConfigureKestrel(this IWebHostBuilder builder)
    {
        EnvUtility.LoadEnvFile();

        var httpPort = DotNetEnv.Env.GetInt("PORT", 0);
        var httpsPort = DotNetEnv.Env.GetInt("HTTPS_PORT", 0);
        var publicHttpsPort = DotNetEnv.Env.GetInt("PUBLIC_HTTPS_PORT", 0);

        builder.ConfigureKestrel(options =>
        {

            if (EnvUtility.IsDevelopment())
            {
                options.ListenAnyIP(int.Parse("1" + httpPort), listenOption =>
                {
                    listenOption.Protocols = HttpProtocols.Http1;
                });
                var certPath = DotNetEnv.Env.GetString("ASPNETCORE_Kestrel__Certificates__Default__Path");
                var certPassword = DotNetEnv.Env.GetString("ASPNETCORE_Kestrel__Certificates__Default__Password");

                options.ListenAnyIP(int.Parse("1" + httpsPort), listenOption =>
                {
                    listenOption.Protocols = HttpProtocols.Http1AndHttp2;
                    // Can't use directly from dotnetenv, have to assign to an variable. Weird bug
                    listenOption.UseHttps(certPath, certPassword);
                });

            }
            else
            {
                options.ListenAnyIP(httpPort, listenOption =>
                {
                    listenOption.Protocols = HttpProtocols.Http1;
                });

                var certificate = X509Certificate2.CreateFromPemFile("/etc/ssl/certs/server-cert.crt", "/etc/ssl/private/private-key.pem");
                options.ListenAnyIP(httpsPort, listenOption =>
                {
                    listenOption.Protocols = HttpProtocols.Http1AndHttp2;
                    // Can't use directly from dotnetenv, have to assign to an variable. Weird bug
                    listenOption.UseHttps(certificate);
                });
            }
        });

        return builder;
    }
}
