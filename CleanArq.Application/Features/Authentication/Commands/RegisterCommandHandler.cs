using CleanArq.Application.Features.Authentication.Commands.Register;
using CleanArq.Application.Features.Authentication.Common;
using CleanArq.Domain.Entities;
using CleanArq.SharedKernel.Interfaces;
using CleanArq.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace CleanArq.Application.Features.Authentication.Commands;

public class RegisterCommandHandler :
    IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var spec = new UserSpecification(command.Email);

        if (await _unitOfWork.Repository<User>().GetBySpecAsync(spec) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
        };

        _unitOfWork.Repository<User>().Add(user);
        await _unitOfWork.CompleteAsync();

        return new AuthenticationResult(
            new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            },
            "Token"
            );
    }
}
