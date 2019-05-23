using EFCore2019.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore2019.Domain.Models.Users
{
    public class UserModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public UserStatus Status { get; set; }

        public string Mobile { get; set; }

        public Guid Avatar { get; set; }

        public int? Gender { get; set; }

        public string UserRoles { get; set; }

        public string Permissions { get; set; }
    }
}
