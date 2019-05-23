using EFCore2019.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore2019.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public UserStatus Status { get; set; }

        public string Mobile { get; set; }

        public Guid Avatar { get; set; }

        public int? Gender { get; set; }

        public string UserRoles { get; set; }
    }
}
