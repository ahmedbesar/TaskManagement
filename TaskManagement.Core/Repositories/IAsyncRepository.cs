using System.Linq.Expressions;
using TaskManagement.Core.Entities;

namespace TaskManagement.Core.Repositories;

public interface IAsyncRepository<T> where T : EntityBase
{
    Task<IReadOnlyList<T>> GetAllAsync(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        List<Expression<Func<T, object>>>? includes = null,
        bool disableTracking = true,
        int? skip = null,
        int? take = null);

    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
