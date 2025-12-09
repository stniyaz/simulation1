using Hackathon.Core.Entities;
using System.Linq.Expressions;

namespace Hackathon.Core.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity, new()
{
    void Remove(T entity);
    Task AddAsync(T entity);
    void UpdateAsync(T entity);
    Task<T> GetByIdAsync(int id);
    Task<int> SaveChangesAsync();
    Task<IEnumerable<T>> GetAllAsync();
    IQueryable<T> GetAllWhereAsync(Expression<Func<T, bool>> expression, params string[]? includes);
}
