using CleanArq.Application.Common.Specifications;

namespace CleanArq.Application.Users.Common.Specifications;

public class UserListPaginatedParams : PaginationParams
{
    public UserListPaginatedParams(string? search, string? sort, int? pageIndex, int? pageSize) : base(pageIndex, pageSize)
    {
        Search = search?.ToLower();
        Sort = sort;
    }

    public string? Search { get; }
    public string? Sort { get; }
}
