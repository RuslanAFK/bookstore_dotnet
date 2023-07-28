using Data.Extensions;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Data.Repositories
{
    public abstract class SearchableRepository<TEntity> : BaseRepository<TEntity> where TEntity : class, ISearchable
    {

        protected SearchableRepository(AppDbContext context) : base(context)
        {
        }
        protected async Task<ListResponse<TEntity>> GetQueriedItemsAsync(Query query, IQueryable<TEntity> items)
        {
            var itemsSearched = items.ApplySearching(query);
            var itemsPaginatedList = await itemsSearched.ApplyPagination(query, 4).ToListAsync();
            var itemsCount = itemsSearched.Count();
            return GenerateListResponse(itemsCount, itemsPaginatedList);
        }

        private ListResponse<TEntity> GenerateListResponse(int count, IList<TEntity> items)
        {
            return new ListResponse<TEntity>
            {
                Count = count,
                Items = items
            };
        }
        protected async Task<TEntity> GetByIdAsync(int id, IQueryable<TEntity> items)
        {
            var item = await items.SingleOrDefaultAsync(e => e.Id == id);
            return GetItemOrThrowNullError(item, id.ToString(), nameof(id));
        }
        protected async Task<TEntity> GetByNameAsync(string name, IQueryable<TEntity> items)
        {
            var item = await items.SingleOrDefaultAsync(e => e.Name == name);
            return GetItemOrThrowNullError(item, name.ToString(), nameof(name));
        }
    }
}