using Domain.Exceptions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Services.Extensions;

public static class SearchExtensions
{
    public static IQueryable<T> ApplySearching<T>(this IQueryable<T> self, Query query)
        where T : ISearchable
    {
        if (string.IsNullOrEmpty(query.Search))
            return self;
        return self.Where(item => item.Name.ToLower().Contains(query.Search.ToLower()));
    }
}