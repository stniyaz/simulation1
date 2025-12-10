using Hackathon.Core.Entities.Common;
using Hackathon.Core.Interfaces.Common;
using Hackathon.Data.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Hackathon.Data.Repositories.Common;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
{

    private readonly AppDbContext _context;
    public Repository(AppDbContext context) => _context = context;
    public DbSet<TEntity> Table => _context.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync(
        Guid id,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool enableTracking = false)
    {
        if (include != null)
        {
            return await GetAll(include: include, enableTracking: enableTracking).FirstOrDefaultAsync(e => e.Id == id);
        }

        if (enableTracking)
        {
            return await Table.FindAsync(id);
        }

        return await Table.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool enableTracking = false)
    {
        if (include != null)
        {
            return await GetAll(predicate: predicate, include: include, enableTracking: enableTracking).FirstOrDefaultAsync();
        }

        if (enableTracking)
        {
            return await Table.FirstOrDefaultAsync(predicate);
        }

        return await Table.AsNoTracking().FirstOrDefaultAsync(predicate);
    }


    public IQueryable<TEntity> GetAll(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        bool enableTracking = false)
    {
        IQueryable<TEntity> query = Table;
        if (!enableTracking)
            query = query.AsNoTracking();
        if (predicate != null)
            query = query.Where(predicate);
        if (include != null)
            query = include(query);
        if (orderBy != null)
            query = orderBy(query);
        return query;
    }

    public IQueryable<TEntity> GetByPaged(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        bool enableTracking = false,
        int pageIndex = 1,
        int pageSize = 20)
    {

        var query = GetAll(predicate, include, orderBy, enableTracking);
        return query.Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize);
    }


    public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await Table.AnyAsync(expression);
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await Table.AddAsync(entity);
        return entity;
    }

    public void Delete(TEntity entity)
    {
        Table.Remove(entity);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        var existingEntity = await Table.FindAsync(entity.Id);
        if (existingEntity == null)
            throw new KeyNotFoundException($"{typeof(TEntity).Name} with ID {entity.Id} not found");

        _context.Entry(existingEntity).CurrentValues.SetValues(entity);

        entity.UpdatedDate = DateTime.UtcNow;

    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

}