using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EFCore2019.Libraries.Utils
{
    public static class EncryptUtil
    {
        public static string SecurityKey = "default";

        public static string EncryptMD5(string context)
        {
            UTF8Encoding Unic = new UTF8Encoding();

            byte[] bytes = Unic.GetBytes(context);

            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] result = md5.ComputeHash(bytes);

            return BitConverter.ToString(result);
        } 
    }
}
