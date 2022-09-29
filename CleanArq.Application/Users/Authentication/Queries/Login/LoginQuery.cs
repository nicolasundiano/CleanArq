using CleanArq.Application.Users.Authentication.Common;
using ErrorOr;
using MediatR;

namespace CleanArq.Application.Users.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;
