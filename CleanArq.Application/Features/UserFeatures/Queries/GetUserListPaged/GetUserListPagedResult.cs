using CleanArq.Application.Features.UserFeatures.Common.Dtos;

namespace CleanArq.Application.Features.UserFeatures.Queries.GetUserListPaged;

public record GetUserListPagedResult(
    int PageIndex,
    int PageSize,
    int Count,
    int PageCount,
    List<UserDto> Users
    );