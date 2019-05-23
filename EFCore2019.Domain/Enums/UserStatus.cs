using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore2019.Domain.Enums
{
    public enum UserStatus
    {
        /// <summary>
        /// Account is not active
        /// </summary>
        NotActived = 1,

        /// <summary>
        /// Account is actived
        /// </summary>
        Actived = 2,

        /// <summary>
        /// Account is disabled by admin
        /// </summary>
        Disabled = 3
    }
}
