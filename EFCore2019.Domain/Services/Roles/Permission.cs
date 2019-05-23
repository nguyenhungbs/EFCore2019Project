using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore2019.Domain.Services.Roles
{
    public class Permission
    {
        public PermissionCode Code { get; set; }

        public string Name { get; set; }

        public string HASH_CODE
        {
            get
            {
                return Code.ToString();
            }
        }

        public int GroupId { get; set; }
    }

    public class PermissionGroup
    {
        public int GroupId { get; set; }

        public string Name { get; set; }

        public List<Permission> Permissions { get; set; }
    }
}
