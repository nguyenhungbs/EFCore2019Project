using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCore2019.Domain.Entities;
using EFCore2019.Domain.Models.PagingInfo;
using EFCore2019.Domain.Models.Users;
using EFCore2019.Domain.Repositories;
using EFCore2019.Libraries.Utils;

namespace EFCore2019.Domain.Services.Roles.Implementation
{
    public class RoleService : IRolesService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public BaseSearchResult<Role> Filter(SearchModel<Role> roleFilterModel)
        {
            return _roleRepository.FindAllPaging(roleFilterModel, null);
        }

        public RoleModel GetById(int id)
        {
            var role = _roleRepository.FindById(id);
            return role.CloneToModel<Role, RoleModel>();
        }

        public List<RoleModel> List(int[] ids)
        {
            var res = _roleRepository.FindAll(r => ids.Contains(r.Id));
            return res.ToList().CloneToListModels<Role, RoleModel>();
        }

        public List<PermissionGroup> ListPermissions()
        {
            return PermissionHelper.GetPermissionGroups();
        }

        public async Task<bool> UpdateAsync(RoleModel role)
        {
            var entity = role.CloneToModel<RoleModel, Role>();
            return await _roleRepository.UpdateAsync(entity);
        }
    }
}
