using Hackathon.Core.Entities;
using Hackathon.Core.Interfaces;
using Hackathon.Data.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hackathon.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _table;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _table = _context.Set<T>();
    }

    public async Task AddAsync(T entity)
        => await _table.AddAsync(entity);

    public async Task<IEnumerable<T>> GetAllAsync()
        => await _table.ToListAsync();

    public IQueryable<T> GetAllWhereAsync(Expression<Func<T, bool>> expression, params string[]? includes)
    {
        var query = _table.AsQueryable();

        query = expression is not null ? query.Where(expression) : query;

        if (includes is not null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return query;
    }

    public async Task<T> GetByIdAsync(int id)
        => await _table.FindAsync(id);

    public void Remove(T entity)
        => _table.Remove(entity);

    public async Task<int> SaveChangesAsync()
        => await _context.SaveChangesAsync();

    public void UpdateAsync(T entity)
        => _table.Update(entity);
}
