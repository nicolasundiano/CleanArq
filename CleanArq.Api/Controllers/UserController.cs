using CleanArq.Application.Common.Models;
using CleanArq.Application.Features.UserFeatures.Commands.UpsertAddress;
using CleanArq.Application.Users.Commands.UpsertAddress;
using CleanArq.Application.Users.Common.Dtos;
using CleanArq.Application.Users.Queries.GetUserListPaged;
using CleanArq.Contracts.User;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CleanArq.Api.Controllers;

[Route("[controller]/[action]")]
public class UserController : ApiController 
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    private readonly string? _userEmail;

    public UserController(ISender mediator, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _mapper = mapper;
        _userEmail = httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
    }

    [HttpPost]
    public async Task<IActionResult> UpsertAddress(UpsertAddressRequest request)
    {
        if (string.IsNullOrEmpty(_userEmail))
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized);
        }

        var command = _mapper.Map<UpsertAddressCommand>(request);
        command.UserEmail = _userEmail;

        ErrorOr<UpsertAddressResult> result = await _mediator.Send(command);

        return result.Match(
            result => Ok(_mapper.Map<UpsertAddressResponse>(result)),
            errors => Problem(errors));
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetUsers([FromQuery] GetUserListPagedRequest request)
    {
        var query = _mapper.Map<GetUserListPagedCommand>(request);

        ErrorOr<PaginatedList<UserDto>> result = await _mediator.Send(query);

        return result.Match(
            result => Ok(result),
            errors => Problem(errors));
    }
}
