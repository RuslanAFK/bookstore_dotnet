using Domain.Models;

namespace Data.Extensions;

public static class Searcher
{
    public static IQueryable<T> ApplySearching<T>(this IQueryable<T> self, Query query)
        where T : ISearchable
    {
        if (query.Search == null)
            return self;
        return self.Where(item => IsContainingSearched(item.Name, query.Search));
    }

    private static bool IsContainingSearched(string container, string searched)
    {
        var containerNormalized = container.ToLower();
        var searchedNormalized = searched.ToLower();
        return containerNormalized.Contains(searchedNormalized);
    }
}