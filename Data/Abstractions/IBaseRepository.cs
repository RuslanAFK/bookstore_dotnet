using System.Linq.Expressions;

namespace Data.Abstractions;

public interface IBaseRepository<TEntity>
{
    IQueryable<TEntity> GetItemsIncluding<TProperty>(IQueryable<TEntity> items,
        Expression<Func<TEntity, TProperty>> func);
    IQueryable<TEntity> GetAll();
    Task AddAsync(TEntity item);
    void Update(TEntity item);
    void Remove(TEntity item);
}