using EFCore2019.Domain.Entities;
using EFCore2019.Domain.Models;
using EFCore2019.Domain.Models.PagingInfo;
using EFCore2019.Domain.Models.Tests;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore2019.Domain.Services.Test
{
    public interface ITestService
    {
        IEnumerable<TestThuModel> GetData();

        bool SaveTestModel(UpdateTestModel model);

        bool DeleteTest(DeleteTestModel model);

        TestThuModel FindTestByCondition(int id);

        BaseSearchResult<TestThu> SearchTest(SearchModel<TestThu> search);
    }
}
