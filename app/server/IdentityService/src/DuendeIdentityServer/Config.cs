using Contract.Utilities;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using DuendeIdentityServer.Services;

namespace DuendeIdentityServer;

public static class Config
{
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
            EnvUtility.LoadEnvFile();

            var reactUrl = DotNetEnv.Env.GetString("REACT_URL", "http://localhost:3000").Trim();

            return [
                new Client
                {
                    ClientId = "react.native",
                    ClientName = "React native",
                    RequireClientSecret = false, // TODO: add secret later
                    AllowedGrantTypes = [GrantType.AuthorizationCode, GrantType.ResourceOwnerPassword],
                    RequirePkce = true,
                    AllowAccessTokensViaBrowser = false,
                    AlwaysIncludeUserClaimsInIdToken = true, // Attach user claim for SPA client
                    AccessTokenLifetime = 2592000,
                    AllowOfflineAccess = true,
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
                }
            ];
        }
    }
}
