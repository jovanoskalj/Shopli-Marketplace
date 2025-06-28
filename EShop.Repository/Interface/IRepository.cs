using EShop.Domain.Domain_Models;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Repository.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Insert(T entity);
        Task<T> InsertAsync(T entity);
        List<T> InsertMany(List<T> entities);
        T Update(T entity);
        Task<T> UpdateAsync(T entity);
        T Delete(T entity);
        Task<T> DeleteAsync(T entity);
        T? GetById(Guid id);
        Task<T?> GetByIdAsync(Guid id);
        IQueryable<T> GetAll();
        E? Get<E>(Expression<Func<T, E>> selector,
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        IEnumerable<E> GetAll<E>(Expression<Func<T, E>> selector,
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
    }
}
