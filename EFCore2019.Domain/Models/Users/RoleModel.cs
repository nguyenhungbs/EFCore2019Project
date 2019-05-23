using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore2019.Domain.Models.Users
{
    public class RoleModel
    {
        public int Id { get; set; }

        public string RoleName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Permissions { get; set; }

        public int? DisplayOrder { get; set; }
    }
}
