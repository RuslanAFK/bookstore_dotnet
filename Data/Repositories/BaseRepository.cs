using Data.Abstractions;

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
        return _dbContext.Set<TEntity>();
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
}