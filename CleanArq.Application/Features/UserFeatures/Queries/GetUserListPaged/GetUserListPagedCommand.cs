using ErrorOr;
using MediatR;

namespace CleanArq.Application.Features.UserFeatures.Queries.GetUserListPaged;

public record GetUserListPagedCommand(
    string? Search,
    string? Sort,
    int? PageIndex,
    int? PageSize
    ) : IRequest<ErrorOr<GetUserListPagedResult>>;