namespace CleanArq.Application.Common.Specifications;

public abstract class PaginationParams
{
    private const int MinPageIndex = 1;
    private const int MinPageSize = 1;
    private const int MaxPageSize = 50;

    private const int DefaultPageIndex = 1;
    private const int DefaultPageSize = 6;

    protected PaginationParams(int? pageIndex, int? pageSize)
    {
        PageIndex = GetPageIndex(pageIndex);
        PageSize = GetPageSize(pageSize);
    }

    public int PageIndex { get; }
    public int PageSize { get; }

    private static int GetPageIndex(int? pageIndex)
    {
        return pageIndex switch
        {
            null => DefaultPageIndex,
            < MinPageIndex => MinPageIndex,
            _ => pageIndex.Value
        };
    }

    private static int GetPageSize(int? pageSize)
    {
        return pageSize switch
        {
            null => DefaultPageSize,
            > MaxPageSize => MaxPageSize,
            < MinPageSize => MinPageSize,
            _ => pageSize.Value
        };
    }
}
