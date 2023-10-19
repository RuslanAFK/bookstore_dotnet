using System.Linq.Expressions;
using Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using Domain.Exceptions;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _dbContext;

    protected BaseRepository(AppDbContext context)
    {
        _dbContext = context;
    }

    public IQueryable<TEntity> GetAll()
    {
        return _dbContext.Set<TEntity>().AsNoTracking();
    }

    public async Task AddAsync(TEntity item)
    {
        await _dbContext.AddAsync(item);
    }

    public void Update(TEntity item)
    {
        _dbContext.Update(item);
    }

    public void Remove(TEntity item)
    {
        _dbContext.Remove(item);
    }

    public IQueryable<TEntity> GetItemsIncluding<TProperty>(IQueryable<TEntity> items, 
        Expression<Func<TEntity, TProperty>> func)
    {
        return items.Include(func);
    }

    protected TEntity GetItemOrThrowNullError(TEntity? item, string propertyValue, string propertyName)
    {
        if (item == null)
            throw new EntityNotFoundException(typeof(TEntity), propertyName, propertyValue);
        return item;
    }
}