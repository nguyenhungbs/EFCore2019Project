using EFCore2019.Domain.Models.Users;
using EFCore2019.Domain.Services.Roles;
using EFCore2019.Domain.Services.Users;
using EFCore2019.Libraries.Utils;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore2019.Authentication.IdentityServer
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IRolesService _rolesService;

        public ResourceOwnerPasswordValidator(IAuthorizationService authorizationService,
            IRolesService rolesService)
        {
            _authorizationService = authorizationService;
            _rolesService = rolesService;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            try
            {
                UserModel userModel = null;

                if (_authorizationService.VerifyPassword(context.UserName, EncryptUtil.EncryptMD5(context.Password), userModel : ref userModel))
                {
                    if (userModel != null)
                    {
                        context.Result = new GrantValidationResult(
                            subject: userModel.Id.ToString(),
                            authenticationMethod: "custom",
                            claims: Config.GetUserClaims(userModel, _rolesService));
                    }
                    else
                    {
                        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist");
                    }
                }
                else
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password");
                }
            }
            catch (Exception)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "Invalid request");             
            }

        }
    }
}
