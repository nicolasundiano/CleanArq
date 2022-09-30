using CleanArq.Application.Users.Authentication.Common;
using ErrorOr;
using MediatR;

namespace CleanArq.Application.Users.Authentication.Commands.ChangePassword;

public record ChangePasswordCommand(
    string CurrentPassword,
    string NewPassword) : IRequest<ErrorOr<ChangePasswordResult>>;
