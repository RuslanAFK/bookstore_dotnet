namespace Data.Abstractions;

public interface IBaseRepository<TEntity>
{
    IQueryable<TEntity> GetAll();
    Task AddAsync(TEntity item);
    void Update(TEntity item);
    void Remove(TEntity item);
}