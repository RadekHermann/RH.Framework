using System;
using System.Collections.Generic;

using IdentityServer4.Models;

namespace RH.IdentityServer
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("resourceapi", "Resource API")
                {
                    Scopes = {new Scope("api.read")}
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    RequireConsent = false,
                    ClientId = "angular_spa",
                    ClientName = "Angular Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "api.read" },
                    RedirectUris = {"http://localhost:5001/auth/signin-callback"}, // test client runs on same host,
                    PostLogoutRedirectUris = new List<string> {"http://localhost:5001/auth/signout-callback"},
                    AllowedCorsOrigins = {"http://localhost:5001" }, // test client runs on same host
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds
                }
            };
        }
    }
}
