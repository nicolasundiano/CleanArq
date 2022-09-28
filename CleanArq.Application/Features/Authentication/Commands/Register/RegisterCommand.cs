using CleanArq.Application.Features.Authentication.Common;
using ErrorOr;
using MediatR;

namespace CleanArq.Application.Features.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;