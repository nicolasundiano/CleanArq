using CleanArq.Application.Common.Helpers;
using CleanArq.Application.Common.Interfaces.Persistence;
using CleanArq.Application.Common.Models;
using CleanArq.Application.Users.Common.Dtos;
using CleanArq.Application.Users.Common.Specifications;
using CleanArq.Domain.Entities.User;
using ErrorOr;
using MediatR;

namespace CleanArq.Application.Users.Queries.GetUserListPaged;

public class GetUserListPagedCommandHandler : IRequestHandler<GetUserListPagedCommand, ErrorOr<PaginatedList<UserDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserListPagedCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<PaginatedList<UserDto>>> Handle(GetUserListPagedCommand request, CancellationToken cancellationToken)
    {
        var userListPagedParams = new UserListPagedParams(request.Search, request.Sort, request.PageIndex, request.PageSize);
        var spec = new UserSpecification(userListPagedParams);
        var countSpec = new UserForCountingSpecification(userListPagedParams);

        var users = await _unitOfWork.Repository<User>().ListAsync(spec);
        var usersCount = await _unitOfWork.Repository<User>().CountAsync(countSpec);

        var usersDto = new List<UserDto>();

        foreach (var user in users)
        {
            usersDto.Add(new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            });
        }

        return new PaginatedList<UserDto>(
            usersDto,
            usersCount,
            userListPagedParams.PageIndex,
            userListPagedParams.PageSize);
    }
}
