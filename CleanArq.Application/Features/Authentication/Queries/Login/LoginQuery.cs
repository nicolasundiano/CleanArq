using CleanArq.Application.Features.Authentication.Common;
using ErrorOr;
using MediatR;

namespace CleanArq.Application.Features.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;
