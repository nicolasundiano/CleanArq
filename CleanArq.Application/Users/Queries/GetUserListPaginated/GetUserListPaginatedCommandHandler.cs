using CleanArq.Application.Common.Interfaces.Persistence;
using CleanArq.Application.Common.Models;
using CleanArq.Application.Users.Common.Dtos;
using CleanArq.Application.Users.Common.Specifications;
using CleanArq.Domain.Entities.User;
using ErrorOr;
using MediatR;

namespace CleanArq.Application.Users.Queries.GetUserListPaginated;

public class GetUserListPaginatedCommandHandler : IRequestHandler<GetUserListPaginatedCommand, ErrorOr<PaginatedList<UserDto>>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserListPaginatedCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<PaginatedList<UserDto>>> Handle(GetUserListPaginatedCommand request, CancellationToken cancellationToken)
    {
        var userListPaginatedParams = new UserListPaginatedParams(request.Search, request.Sort, request.PageIndex, request.PageSize);
        var spec = new UserSpecification(userListPaginatedParams);
        var countSpec = new UserForCountingSpecification(userListPaginatedParams);

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
            userListPaginatedParams.PageIndex,
            userListPaginatedParams.PageSize);
    }
}
