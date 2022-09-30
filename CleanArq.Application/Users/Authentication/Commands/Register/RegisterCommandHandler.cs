using CleanArq.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using CleanArq.Application.Common.Interfaces.Authentication;
using CleanArq.Application.Common.Interfaces.Persistence;
using CleanArq.Domain.Entities.User;
using CleanArq.Application.Features.UserFeatures.Authentication.Commands.Register;
using CleanArq.Application.Users.Authentication.Common;
using CleanArq.Application.Users.Common.Specifications;
using CleanArq.Application.Users.Common.Dtos;

namespace CleanArq.Application.Users.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterCommandHandler(IUnitOfWork unitOfWork, IAuthService authService, IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        var spec = new UserSpecification(command.Email);

        if (await _unitOfWork.Repository<User>().GetAsync(spec) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        if (await _authService.UserExists(command.Email))
        {
            return Errors.User.DuplicateEmail;
        }

        var user = new User(command.FirstName, command.LastName, command.Email);

        _unitOfWork.Repository<User>().Add(user);
        await _unitOfWork.CompleteAsync();

        await _authService.CreateUserAsync(user, command.Password);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            },
            token
            );
    }
}
