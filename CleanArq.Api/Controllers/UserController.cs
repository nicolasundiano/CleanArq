using CleanArq.Application.Common.Models;
using CleanArq.Application.Users.Commands.UpsertAddress;
using CleanArq.Application.Users.Common.Dtos;
using CleanArq.Application.Users.Queries.GetUserListPaginated;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArq.Api.Controllers;

[Route("[controller]/[action]")]
public class UserController : ApiController 
{
    private readonly ISender _mediator;

    public UserController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> UpsertAddress(UpsertAddressCommand command)
    {
        ErrorOr<AddressDto> result = await _mediator.Send(command);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors));
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetUsers([FromQuery] GetUserListPaginatedCommand query)
    {
        ErrorOr<PaginatedList<UserDto>> result = await _mediator.Send(query);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors));
    }
}
