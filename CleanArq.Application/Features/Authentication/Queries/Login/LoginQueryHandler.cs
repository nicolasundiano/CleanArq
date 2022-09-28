using CleanArq.Application.Common.Interfaces.Authentication;
using CleanArq.Application.Features.Authentication.Common;
using CleanArq.SharedKernel.Interfaces;
using CleanArq.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using CleanArq.Domain.Entities;

namespace CleanArq.Application.Features.Authentication.Queries.Login;

public class LoginQueryHandler :
    IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthRepository _authRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginQueryHandler(IUnitOfWork unitOfWork, IAuthRepository authRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _authRepository = authRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var spec = new UserSpecification(request.Email);

        if (!await _authRepository.PasswordSignInAsync(request.Email, request.Password))
        {
            return Errors.Authentication.InvalidCredentials;
        }

        if (await _unitOfWork.Repository<User>().GetBySpecAsync(spec) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            },
            token);
    }
}
