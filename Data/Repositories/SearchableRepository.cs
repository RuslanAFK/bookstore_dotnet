using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Data.Repositories;

public abstract class SearchableRepository<TEntity> : BaseRepository<TEntity> where TEntity : class, ISearchable
{

    protected SearchableRepository(AppDbContext context) : base(context)
    {
    }

    protected async Task<TEntity> GetByIdAsync(int id, IQueryable<TEntity> items)
    {
        var item = await items.SingleOrDefaultAsync(e => e.Id == id);
        return GetItemOrThrowNullError(item, id.ToString(), nameof(id));
    }
    protected async Task<TEntity> GetByNameAsync(string name, IQueryable<TEntity> items)
    {
        var item = await items.SingleOrDefaultAsync(e => e.Name == name);
        return GetItemOrThrowNullError(item, name, nameof(name));
    }
}