using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Domain.Abstractions;
using Domain.Exceptions;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _dbContext;

    protected BaseRepository(AppDbContext context)
    {
        _dbContext = context;
    }

    protected IQueryable<TEntity> GetAll()
    {
        return _dbContext.Set<TEntity>();
    }

    public async Task AddAsync(TEntity item)
    {

        await _dbContext.Set<TEntity>().AddAsync(item);
    }

    public void Update(TEntity item)
    {
        _dbContext.Set<TEntity>().Update(item);
    }

    public void Remove(TEntity item)
    {
        _dbContext.Set<TEntity>().Remove(item);
    }

    protected IQueryable<TEntity> GetItemsIncluding<TProperty>(IQueryable<TEntity> items, 
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