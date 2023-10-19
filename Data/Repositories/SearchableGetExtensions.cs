using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Data.Repositories;

public static class SearchableGetExtensions
{
    public static async Task<TEntity> GetByIdAsync<TEntity>(this IQueryable<TEntity> items, int id)
        where TEntity : class, ISearchable
    {
        var item = await items.FirstOrDefaultAsync(e => e.Id == id);
        return item ?? throw new EntityNotFoundException(typeof(TEntity), nameof(id));
    }
    public static async Task<TEntity> GetByNameAsync<TEntity>(this IQueryable<TEntity> items, string name)
        where TEntity : class, ISearchable
    {
        var item = await items.FirstOrDefaultAsync(e => e.Name == name);
        return item ?? throw new EntityNotFoundException(typeof(TEntity), nameof(name));
    }
}