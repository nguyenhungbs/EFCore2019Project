using EFCore2019.Domain.Enums;
using EFCore2019.Domain.Services.Roles;
using EFCore2019.Domain.Services.Users;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore2019.Authentication.IdentityServer
{
    public class ProfileService : IProfileService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IRolesService _rolesService;

        public ProfileService(IAuthorizationService authorizationService, IRolesService rolesService)
        {
            _authorizationService = authorizationService;
            _rolesService = rolesService;
        }

        //Get user profile date in terms of claims when calling /connect/userinfo
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                if (!string.IsNullOrEmpty(context.Subject.Identity.Name))
                {
                    var user = await _authorizationService.GetByIdAsync(Convert.ToInt32(context.Subject.Identity.Name));

                    if (user != null)
                    {
                        var claims = Config.GetUserClaims(user, _rolesService);

                        context.IssuedClaims = claims.Where(c => context.RequestedClaimTypes.Contains(c.Type)).ToList();
                    }
                }
                else
                {
                    var userId = context.Subject.Claims.FirstOrDefault(c => c.Type == "sub");

                    if (!string.IsNullOrEmpty(userId?.Value) && long.Parse(userId.Value) > 0)
                    {
                        var user = await _authorizationService.GetByIdAsync(int.Parse(userId.Value));

                        if (user!= null)
                        {
                            var claims = Config.GetUserClaims(user, _rolesService);

                            context.IssuedClaims = claims.ToList();
                        }
                    }       
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            try
            {
                var userId = context.Subject.Claims.FirstOrDefault(c => c.Type == "user_id");

                if (!string.IsNullOrEmpty(userId?.Value) && int.Parse(userId.Value) > 0)
                {
                    var user = await _authorizationService.GetByIdAsync(int.Parse(userId.Value));

                    if (user != null)
                    {
                        context.IsActive = user.Status == UserStatus.Actived ? true : false;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
