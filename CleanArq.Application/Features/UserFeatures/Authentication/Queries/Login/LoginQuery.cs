using CleanArq.Application.Features.UserFeatures.Authentication.Common;
using ErrorOr;
using MediatR;

namespace CleanArq.Application.Features.UserFeatures.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<ErrorOr<AuthenticationResult>>;
