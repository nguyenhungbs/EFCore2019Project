using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore2019.Domain.Services.Roles
{
    public enum PermissionGroupCode
    {
        SYSTEM = 1,
        NORMAL = 2
    }
    public enum PermissionCode
    {
        [PermissionDetails(PermissionGroupCode.SYSTEM, "Quản lý người dùng")]
        MANAGE_USERS = 1,
        [PermissionDetails(PermissionGroupCode.SYSTEM,"Quản lý vai trò")]
        MANAGE_ROLES = 5
    }
}
