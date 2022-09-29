namespace CleanArq.Contracts.User;

public record GetUserListPagedRequest(
    string? Search,
    string? Sort,
    int? PageIndex,
    int? PageSize
    );