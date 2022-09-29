using CleanArq.Application.Users.Common.Dtos;

namespace CleanArq.Application.Users.Queries.GetUserListPaged;

public record GetUserListPagedResult(
    int PageIndex,
    int PageSize,
    int Count,
    int PageCount,
    List<UserDto> Users
    );