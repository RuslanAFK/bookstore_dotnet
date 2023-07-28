namespace Domain.Abstractions
{
    public interface IBaseRepository<in TEntity>
    {
        Task AddAsync(TEntity item);
        void Update(TEntity item);
        void Remove(TEntity item);
    }
}
