using Contract.Extension;
using Contract.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Security.Cryptography.X509Certificates;

namespace Contract.Extension;

public static class KestrelExtension
{
    public static WebApplicationBuilder ConfigureKestrel(this WebApplicationBuilder builder)
    {
        EnvUtility.LoadEnvFile();

        var httpPort = DotNetEnv.Env.GetInt("PORT", 0);
        var httpsPort = DotNetEnv.Env.GetInt("HTTPS_PORT", 0);

        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(httpPort, listenOption =>
            {
                listenOption.Protocols = HttpProtocols.Http1;
            });

            if (EnvUtility.IsDevelopment())
            {
                var certPath = DotNetEnv.Env.GetString("ASPNETCORE_Kestrel__Certificates__Default__Path");
                var certPassword = DotNetEnv.Env.GetString("ASPNETCORE_Kestrel__Certificates__Default__Password");

                options.ListenAnyIP(httpsPort, listenOption =>
                {
                    listenOption.Protocols = HttpProtocols.Http1AndHttp2;
                    // Can't use directly from dotnetenv, have to assign to an variable. Weird bug
                    listenOption.UseHttps(certPath, certPassword);
                });

            }
            else
            {
                var certificate = X509Certificate2.CreateFromPemFile("/etc/ssl/certs/server-cert.crt", "/etc/ssl/private/private-key.pem");
                options.ListenAnyIP(httpsPort, listenOption =>
                {
                    listenOption.Protocols = HttpProtocols.Http1AndHttp2;
                    // Can't use directly from dotnetenv, have to assign to an variable. Weird bug
                    listenOption.UseHttps(certificate);
                });

            }

            options.ConfigureHttpsDefaults(httpsOptions =>
            {
                httpsOptions.ServerCertificate =
                    X509Certificate2
                        .CreateFromEncryptedPemFile(
                            "/path/to/cert.pem",
                            "",
                            "/path/to/key.pem");

            });


        });

        return builder;
    }
}
