using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EFCore2019.Domain.Entities;
using EFCore2019.Domain.Models.PagingInfo;
using Microsoft.EntityFrameworkCore;

namespace EFCore2019.Domain.Data.Common.Implementation
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AppDbContext _appDbContext;
        public RepositoryBase(AppDbContext appDbContext, IDbConnection connection)
        {
            Connection = connection;
            _appDbContext = appDbContext;
        }

        public AppDbContext GetDbContext()
        {
            return _appDbContext;
        }

        public IDbConnection Connection { get; }

        public IDbTransaction BeginTransaction()
        {
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
            return Connection.BeginTransaction();
        }

        public T FindById(object id)
        {
            return _appDbContext.Set<T>().Find(id);
        }

        public async Task<T> FindByIdAsync(object id)
        {
            return await _appDbContext.Set<T>().FindAsync(id);
        }

        public T Find(Expression<Func<T, bool>> expression)
        {
            return _appDbContext.Set<T>().SingleOrDefault(expression);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _appDbContext.Set<T>().SingleOrDefaultAsync(expression);
        }

        public IEnumerable<T> FindAll()
        {
            return _appDbContext.Set<T>().ToList();
        }    

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> expression)
        {
            if (expression != null)
                return _appDbContext.Set<T>().Where(expression).ToList();
            else
                return _appDbContext.Set<T>().ToList();
        }


        public BaseSearchResult<T> FindAllPaging(SearchModel<T> searchModel)
        {
            var result = new BaseSearchResult<T>();
            var list = _appDbContext.Set<T>().ToList();

            result.Records = list.Skip(searchModel.PageSize * (searchModel.PageIndex - 1)).Take(searchModel.PageSize).ToList();
            result.TotalRecord = list.Count();
            result.PageIndex = searchModel.PageIndex;
            result.PageSize = searchModel.PageSize;
            result.PageCount = result.TotalRecord / result.PageSize + (result.TotalRecord % result.PageSize > 0 ? 1 : 0);

            return result;           
        }

        public BaseSearchResult<T> FindAllPaging(SearchModel<T> searchModel, Expression<Func<T, bool>> expression)
        {
            var result = new BaseSearchResult<T>();
            var list = _appDbContext.Set<T>().Where(expression).ToList();

            result.Records = list.Skip(searchModel.PageSize * (searchModel.PageIndex - 1)).Take(searchModel.PageSize).ToList();
            result.TotalRecord = list.Count();
            result.PageIndex = searchModel.PageIndex;
            result.PageSize = searchModel.PageSize;
            result.PageCount = result.TotalRecord / result.PageSize + (result.TotalRecord % result.PageSize > 0 ? 1 : 0);

            return result;
        }

        public BaseSearchResult<T> FindAllPaging(SearchModel<T> searchModel, Expression<Func<T, bool>> expression, Expression<Func<T, object>> orderBy)
        {
            var result = new BaseSearchResult<T>();
            var list = new List<T>();
            if (searchModel.SortDesc)
            {
                list = _appDbContext.Set<T>().Where(expression).OrderByDescending(orderBy).ToList();
            }          
            else
            {
                list = _appDbContext.Set<T>().Where(expression).OrderBy(orderBy).ToList();
            }

            result.Records = list.Skip(searchModel.PageSize * (searchModel.PageIndex - 1)).Take(searchModel.PageSize).ToList();
            result.TotalRecord = list.Count();
            result.PageIndex = searchModel.PageIndex;
            result.PageSize = searchModel.PageSize;
            result.PageCount = result.TotalRecord / result.PageSize + (result.TotalRecord % result.PageSize > 0 ? 1 : 0);

            return result;
        }

        public bool Any(Expression<Func<T, bool>> expression)
        {
            return _appDbContext.Set<T>().Any(expression);
        }

        public bool Insert(T entity)
        {
            _appDbContext.Set<T>().Add(entity);
            return SaveChange();
        }

        public bool Insert(T entity, IDbTransaction transaction)
        {
            _appDbContext.Set<T>().Add(entity);
            return SaveChange();
        }

        public bool BulkInsert(IEnumerable<T> insertList)
        {
            foreach (T item in insertList)
            {
                _appDbContext.Set<T>().Add(item);
            }
            return SaveChange();
        }

        public bool Update(T entity)
        {
            _appDbContext.Set<T>().Update(entity);
            return SaveChange();
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _appDbContext.Set<T>().Update(entity);
            return await SaveChangeAsync();
        }

        public bool BulkUpdate(IEnumerable<T> listUpdate)
        {
            foreach (T item in listUpdate)
            {
                _appDbContext.Set<T>().Attach(item);
                _appDbContext.Entry<T>(item).State = EntityState.Modified;
            }
            return SaveChange();
           
        }

        public bool Delete(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
            return SaveChange();
        }

        public bool BulkDelete(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> result = _appDbContext.Set<T>().Where(expression);

            foreach (T item in result)
            {
                _appDbContext.Set<T>().Remove(item);
            }
            return SaveChange();
        }

        public bool SaveChange()
        {
             return _appDbContext.SaveChanges() > 0 ? true : false;
        }

        public async Task<bool> SaveChangeAsync()
        {
            return await _appDbContext.SaveChangesAsync() > 0 ? true : false;
        }

        
    }
}
