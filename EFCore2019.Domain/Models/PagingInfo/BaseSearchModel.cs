using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore2019.Domain.Models.PagingInfo
{
    public class BaseSearchModel
    {
        public string SortBy { get; set; }

        public bool SortDesc { get; set; }

        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 20;
    }
}
