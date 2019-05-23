using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore2019.Domain.Entities
{
    public class Role : BaseEntity
    {
        public int RoleName { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Permissions { get; set; }

        public int? DisplayOrder { get; set; }
    }
}
