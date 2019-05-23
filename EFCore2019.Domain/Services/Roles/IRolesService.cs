using EFCore2019.Domain.Entities;
using EFCore2019.Domain.Models.PagingInfo;
using EFCore2019.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFCore2019.Domain.Services.Roles
{
    public interface IRolesService
    {
        List<PermissionGroup> ListPermissions();

        List<RoleModel> List(int[] ids);

        BaseSearchResult<Role> Filter(SearchModel<Role> roleFilterModel);

        RoleModel GetById(int id);

        Task<bool> UpdateAsync(RoleModel role);
    }
}
