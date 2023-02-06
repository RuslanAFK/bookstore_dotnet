using BookStoreServer.Core.Models;

namespace BookStoreServer.Extensions;

public static class QueryExtensions
{
    private const int DefaultPage = 1;
    private const int DefaultPageSize = 10;
    private const int MaxPage = 1000;
    private const int MaxPageSize = 100;

    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> self, QueryObject queryObject, 
        int defaultPageSize=DefaultPageSize)
    {
        var page = queryObject.Page ?? DefaultPage;
        var pageSize = queryObject.PageSize ?? defaultPageSize;
        page = page is > 0 and < MaxPage ? page : DefaultPage;
        pageSize = pageSize is > 0 and  < MaxPageSize ? pageSize : defaultPageSize;
        
        return self.Skip((page - 1) * pageSize).Take(pageSize);
    }

}