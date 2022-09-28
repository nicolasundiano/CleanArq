namespace CleanArq.Application.Common.Helpers;

public static class PaginationHelper
{
    public static int GetSkip(int pageSize, int pageIndex) =>
        pageSize * (pageIndex - 1);

    public static int GetPageCount(int totalCount, int pageSize) =>
        Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalCount) / Convert.ToDecimal(pageSize)));
}
