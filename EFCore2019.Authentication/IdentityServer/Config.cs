using EFCore2019.Authentication.Configuration;
using EFCore2019.Authentication.Helpers;
using EFCore2019.Domain.Models.Users;
using EFCore2019.Domain.Services.Roles;
using IdentityModel;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EFCore2019.Authentication.IdentityServer
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("efcore","EFCore 2019")
                {

                }
            };
        }
        public static IEnumerable<Client> GetClients(AppSettings appSettings)
        {
            var uris = appSettings.ClientAppRedirectUri;

            //client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("m9wfTF5ZXCvL9u5AOIXBK1zWe9Kd9Kzb".Sha256())
                    },
                    AllowedScopes = { "efcore" }
                },

                // resource owner password grant client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    RedirectUris = new List<string>(uris),
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes =
                    {
                        "efcore",
                        "openid",
                        "profile",
                        "offline_access"
                    },
                    AllowOfflineAccess = true,
                    Enabled = true,
                    AllowAccessTokensViaBrowser = true,
                    AllowedCorsOrigins = new List<string>(uris),
                    AccessTokenType = AccessTokenType.Jwt,
                    IdentityTokenLifetime = 3000,
                    AccessTokenLifetime = 3600*24*30,
                    AuthorizationCodeLifetime = 300
                }
            };
        }

        public static Claim[] GetUserClaims(UserModel user, IRolesService rolesService)
        {
            string result = string.Empty;
            if (!string.IsNullOrWhiteSpace(user.UserRoles))
            {
                var ids = user.UserRoles.ParseIds();
                var roles = rolesService.List(ids);
                var permissions = new List<int>();
                foreach (var role in roles)
                {
                    if (!string.IsNullOrEmpty(role.Permissions))
                    {
                        ids = role.Permissions.ParseIds();
                        permissions.AddRange(ids);
                    }
                }
                var res = permissions.ToArray().RemoveDuplicates();
                result = string.Join(";", res);
                if (!string.IsNullOrEmpty(result))
                    result = ";" + result + ";";
            }

            return new Claim[]
            {
                new Claim("user_id", user.Id.ToString() ?? ""),
                new Claim(JwtClaimTypes.Name, user.FullName ?? string.Empty),
                new Claim(JwtClaimTypes.Email, user.Email ??""),

                new Claim("permissions",result)
            };
        }
    }
}
