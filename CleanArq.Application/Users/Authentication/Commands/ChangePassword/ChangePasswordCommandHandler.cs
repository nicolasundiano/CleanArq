using CleanArq.Application.Common.Interfaces.Authentication;
using CleanArq.Application.Users.Authentication.Common;
using CleanArq.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CleanArq.Application.Users.Authentication.Commands.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ErrorOr<ChangePasswordResult>>
{
    private readonly string? _userEmail;
    private readonly IAuthService _authService;

    public ChangePasswordCommandHandler(IHttpContextAccessor httpContextAccessor, IAuthService authService)
    {
        _userEmail = httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
        _authService = authService;
    }

    public async Task<ErrorOr<ChangePasswordResult>> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(_userEmail))
        {
            return Errors.User.NotAuthenticated;
        }

        var result = await _authService.ChangePasswordAsync(_userEmail, command.CurrentPassword, command.NewPassword);

        if (!result)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        return new ChangePasswordResult(result);
    }
}
