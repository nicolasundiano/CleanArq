using CleanArq.Application.Common.Models;
using CleanArq.Application.Users.Common.Dtos;
using ErrorOr;
using MediatR;

namespace CleanArq.Application.Users.Queries.GetUserListPaginated;

public record GetUserListPaginatedCommand(
    string? Search,
    string? Sort,
    int? PageIndex,
    int? PageSize
    ) : IRequest<ErrorOr<PaginatedList<UserDto>>>;