using Query = Domain.Models.Query;

namespace Data.Extensions;

public static class Paginator
{
    private const int DefaultPage = 1;
    private const int DefaultItemsPerPage = 10;
    private const int MaxPage = 1000;
    private const int MaxItemsPerPage = 100;

    public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> self, Query query, 
        int defaultItemsPerPage=DefaultItemsPerPage)
    {
        var currentPage = GetPositiveThresholdValue(DefaultPage, MaxPage, query.Page);
        var itemsPerPage = GetPositiveThresholdValue(defaultItemsPerPage, MaxItemsPerPage, query.PageSize);

        var itemsToSkip = CalculateItemsToSkip(currentPage, itemsPerPage);
        return self.Skip(itemsToSkip).Take(itemsPerPage);
    }

    private static int GetPositiveThresholdValue(int defaultValue, int maxThreshold, int? desiredValue)
    {
        var integer = new PositiveInteger(defaultValue);
        integer.SetMaxThreshold(maxThreshold);
        return integer.GetValue(desiredValue);
    }

    private static int CalculateItemsToSkip(int currentPage, int itemsPerPage)
    {
        var prevPage = currentPage - 1;
        return prevPage * itemsPerPage;
    }
}