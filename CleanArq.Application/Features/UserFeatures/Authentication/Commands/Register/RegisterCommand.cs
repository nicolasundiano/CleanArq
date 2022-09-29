using CleanArq.Application.Features.UserFeatures.Authentication.Common;
using ErrorOr;
using MediatR;

namespace CleanArq.Application.Features.UserFeatures.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;