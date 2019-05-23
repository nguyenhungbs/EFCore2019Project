using EFCore2019.Domain.Entities;
using EFCore2019.Domain.Models;
using EFCore2019.Domain.Models.PagingInfo;
using EFCore2019.Domain.Models.Tests;
using EFCore2019.Domain.Repositories.Test;
using EFCore2019.Libraries.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace EFCore2019.Domain.Services.Test
{
    public class TestService : ITestService
    {

        private readonly ITestRepository _testRepository;

        public TestService(ITestRepository testRepository, AppDbContext appDbContext)
        {
            _testRepository = testRepository;
        }

        public BaseSearchResult<TestThu> SearchTest(SearchModel<TestThu> search)
        {
            var start = new BaseSearchResult<TestThu>();
           
            var result = _testRepository.FindAllPaging(search, c => c.Id > 2, x=>x.Id);
            return result;
        }


        public TestThuModel FindTestByCondition(int id)
        {
            return _testRepository.FindAll(c => c.Id == id).FirstOrDefault().CloneToModel<TestThu, TestThuModel>();
        }

        public IEnumerable<TestThuModel> GetData()
        {
            var entities = _testRepository.FindAll(null);
            return entities.ToList().CloneToListModels<TestThu, TestThuModel>();
        }

        public bool SaveTestModel(UpdateTestModel model)
        {
            var result = true;
            using (var context = _testRepository.GetDbContext())
            {
                using (var trans = context.Database.BeginTransaction())
                {
                    try
                    {                      
                        if (!model.Id.HasValue)
                        {
                            var modelAdd = new TestThu { Name = model.Name };
                            _testRepository.Insert(modelAdd);
                        }
                        else
                        {
                            _testRepository.Update(new TestThu { Id = model.Id.Value, Name = model.Name });
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw ex;
                    }
                    trans.Commit();
                }
            }
            return result;
            //using (var context = new AppDbContext())
            //{

            //}

        }

        public bool DeleteTest(DeleteTestModel model)
        {
            _testRepository.Delete(new TestThu { Id = model.Id });
            return true;
        }


    }
}
