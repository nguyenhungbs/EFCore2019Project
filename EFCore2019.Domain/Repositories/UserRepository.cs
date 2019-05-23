using EFCore2019.Domain.Data.Common;
using EFCore2019.Domain.Data.Common.Implementation;
using EFCore2019.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EFCore2019.Domain.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {
    }

    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly IDbConnection connection;

        public UserRepository(AppDbContext appDbContext, IDbConnection connection) 
            : base(appDbContext, connection)
        {
            this.connection = connection;
        }
    }
}
