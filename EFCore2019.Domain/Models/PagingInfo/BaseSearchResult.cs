using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EFCore2019.Domain.Models.PagingInfo
{
    public class BaseSearchResult<R> where R : class
    {

        public List<R> Records { get; set; }/* = new List<R>();*/

        public int TotalRecord { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int PageCount { get; set; }
        
    } 
}
