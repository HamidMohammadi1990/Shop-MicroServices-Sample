using Ordering.Domain.Common;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ordering.Infrastructure.Persistence;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Infrastructure.Repositories;

public class BaseRepository<T>(OrderDbContext context) : IAsyncRepository<T> where T : BaseEntity, new()
{
    protected readonly OrderDbContext _context = context;
    private DbSet<T> entity = context.Set<T>();

    public async Task<T> CreateAsync(T entity)
    {
        context.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        context.Remove(entity);
        await context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await entity.ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await entity.Where(predicate).ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null,
        bool disableTracking = true)
    {
        var query = entity.AsQueryable();

        if (disableTracking)
            query = entity.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(includeString))
            query = entity.Include(includeString);

        if (predicate is not null)
            query = entity.Where(predicate);

        if (orderBy is not null)
            return await orderBy(entity).ToListAsync();

        return await entity.ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>,
        IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null,
        bool disableTracking = true)
    {
        var query = entity.AsQueryable();

        if (disableTracking)
            query = entity.AsNoTracking();

        if (includes is not null)
            query = includes.Aggregate(query, (current, include) => current.Include(include));

        if (predicate is not null)
            query = entity.Where(predicate);

        if (orderBy is not null)
            return await orderBy(entity).ToListAsync();

        return await entity.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await entity.FindAsync(id);
    }

    public async Task UpdateAsync(T model)
    {
        entity.Update(model);
        await context.SaveChangesAsync();        
    }
}