using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore2019.Authentication.Configuration
{
    public class AppSettings
    {
        public string ApiServer { get; set; }

        public string AuthenticationServer { get; set; }

        public string[] ClientAppRedirectUri { get; set; }
    }
}
