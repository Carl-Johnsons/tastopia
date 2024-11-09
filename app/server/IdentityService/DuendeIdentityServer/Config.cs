using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using DuendeIdentityServer.Services;
using Serilog;

namespace DuendeIdentityServer;

public static class Config
{
    public static string GetConnectionString()
    {
        EnvUtil.LoadEnvFile();
        var host = DotNetEnv.Env.GetString("HOST", "localhost").Trim();

        var port = DotNetEnv.Env.GetString("DB_PORT", "2001").Trim();
        var db = DotNetEnv.Env.GetString("DB", "Not found").Trim();
        var user = DotNetEnv.Env.GetString("POSTGRES_USER", "Not found").Trim();
        var pwd = DotNetEnv.Env.GetString("POSTGRES_PASSWORD", "Not found").Trim();
        var connectionString = $"Host={host};Port={port};Database={db};Username={user};Password{pwd};";

        Log.Information(connectionString);
        return connectionString;
    }
    public static IEnumerable<IdentityResource> IdentityResources =>
               [
                   new IdentityResources.OpenId(),
                   new IdentityResources.Profile(),
                   new IdentityResources.Phone(),
                   new IdentityResources.Email(),
               ];

    public static IEnumerable<ApiScope> ApiScopes =>
        [
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
        ];

    public static IEnumerable<Client> Clients
    {
        get
        {
            EnvUtil.LoadEnvFile();
            
            var reactUrl = DotNetEnv.Env.GetString("REACT_URL", "http://localhost:3000").Trim();

            return [
                new Client
                {
                    ClientId = "react.spa",
                    ClientName = "React SPA",
                    RequireClientSecret = false, // TODO: add secret later
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    AllowAccessTokensViaBrowser = false,
                    AlwaysIncludeUserClaimsInIdToken = true, // Attach user claim for SPA client
                    AccessTokenLifetime = 2592000,
                    RedirectUris = {
                       $"{reactUrl}/api/auth/callback/duende-identityserver6",
                       "https://www.getpostman.com/oauth2/callback"
                    },
                    PostLogoutRedirectUris ={reactUrl},
                    AllowedCorsOrigins = { reactUrl, "https://www.getpostman.com" },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.LocalApi.ScopeName,
                    },
                },
                new Client{
                    ClientId="android.client",
                    ClientName = "Android client",
                    AllowedGrantTypes= GrantTypes.Code,
                    RequirePkce = true,
                    RequireConsent = false,
                    RequireClientSecret=false, // TODO: add secret later
                    RefreshTokenUsage = TokenUsage.ReUse,
                    RedirectUris = {
                       "https://www.getpostman.com/oauth2/callback"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Phone,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.LocalApi.ScopeName,
                        IdentityServerConstants.StandardScopes.OfflineAccess
                    },
                    AllowOfflineAccess = true
                }
            ];
        }
    }
}
