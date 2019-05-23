using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore2019.Authentication.Helpers
{
    public static class StringHelper
    {
        public static string[] RemoveDuplicates(this string[] s)
        {
            HashSet<string> set = new HashSet<string>(s);
            string[] result = new string[set.Count];
            set.CopyTo(result);
            return result;
        }

        public static int[] RemoveDuplicates(this int[] s)
        {
            HashSet<int> set = new HashSet<int>(s);
            int[] result = new int[set.Count];
            set.CopyTo(result);
            return result;
        }

        /// <summary>
        /// Chuyển chuỗi định dạng 3;4;5;6 thành list int
        /// </summary>
        /// <param name="stringFormat"></param>
        /// <returns></returns>
        public static int[] ParseIds(this string stringFormat)
        {
            if (string.IsNullOrEmpty(stringFormat) || !stringFormat.Contains(";")) return null;

            var ids = stringFormat.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(c => Convert.ToInt32(c)).ToArray();
            return ids;
        }
    }
}
