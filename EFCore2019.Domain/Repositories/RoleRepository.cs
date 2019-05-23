using EFCore2019.Domain.Data.Common;
using EFCore2019.Domain.Data.Common.Implementation;
using EFCore2019.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EFCore2019.Domain.Repositories
{
    public interface IRoleRepository : IRepositoryBase<Role>
    {
    }

    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        private readonly IDbConnection connection;

        public RoleRepository(AppDbContext appDbContext, IDbConnection connection) 
            : base(appDbContext, connection)
        {
            this.connection = connection;
        }
       
    }
}
