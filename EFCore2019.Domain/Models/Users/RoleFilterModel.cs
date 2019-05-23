using EFCore2019.Domain.Models.PagingInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore2019.Domain.Models.Users
{
    public class RoleFilterModel : BaseSearchModel
    {
        public string RoleName { get; set; }
    }
}
