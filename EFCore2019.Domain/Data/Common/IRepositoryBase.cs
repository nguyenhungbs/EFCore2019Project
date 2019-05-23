using EFCore2019.Domain.Entities;
using EFCore2019.Domain.Models.PagingInfo;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EFCore2019.Domain.Data.Common
{
    public interface IRepositoryBase<T> where T : class
    {
        AppDbContext GetDbContext();

        T FindById(object id);

        Task<T> FindByIdAsync(object id);

        T Find(Expression<Func<T, bool>> expression);

        Task<T> FindAsync(Expression<Func<T, bool>> expression);

        IEnumerable<T> FindAll(Expression<Func<T, bool>> expression);

        BaseSearchResult<T> FindAllPaging(SearchModel<T> searchModel, Expression<Func<T,bool>> expression);

        BaseSearchResult<T> FindAllPaging(SearchModel<T> searchModel, Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderBy);

        bool Any(Expression<Func<T, bool>> expression);

        bool Insert(T entity);

        bool Insert(T entity, IDbTransaction transaction);

        bool BulkInsert(IEnumerable<T> insertList);

        bool Update(T entity);

        Task<bool> UpdateAsync(T entity);

        bool BulkUpdate(IEnumerable<T> updateList);

        bool Delete(T entity);

        bool BulkDelete(Expression<Func<T, bool>> expression);

    }
}
