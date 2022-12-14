using CleanArq.Application.Features.UserFeatures.Authentication.Commands.Register;
using CleanArq.Application.Users.Authentication.Commands.ChangePassword;
using CleanArq.Application.Users.Authentication.Common;
using CleanArq.Application.Users.Authentication.Queries.Login;
using CleanArq.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArq.Api.Controllers;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;

    public AuthenticationController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(command);

        return authResult.Match(
            authResult => Ok(authResult),
            errors => Problem(errors));
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginQuery query)
    {
        var authResult = await _mediator.Send(query);

        if (authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized,
                title: authResult.FirstError.Description);
        }

        return authResult.Match(
            authResult => Ok(authResult),
            errors => Problem(errors));
    }

    [HttpPut("changePassword")]
    public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
    {
        var result = await _mediator.Send(command);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors));
    }
}
