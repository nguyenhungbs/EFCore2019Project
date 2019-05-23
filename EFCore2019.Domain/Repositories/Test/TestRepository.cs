using EFCore2019.Domain.Data;
using EFCore2019.Domain.Data.Common;
using EFCore2019.Domain.Data.Common.Implementation;
using EFCore2019.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EFCore2019.Domain.Repositories.Test
{
    public interface ITestRepository : IRepositoryBase<TestThu>
    {
    }

    public class TestRepository : RepositoryBase<TestThu>, ITestRepository
    {
        private readonly IDbConnection dbConnection;

        public TestRepository(AppDbContext appDbContext, IDbConnection dbConnection) 
            : base(appDbContext, dbConnection)
        {
            this.dbConnection = dbConnection;
        }
    }
}
