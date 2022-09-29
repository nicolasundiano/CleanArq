using CleanArq.Application.Common.Interfaces.Persistence;
using CleanArq.Domain.Entities.User;
using CleanArq.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using CleanArq.Application.Users.Common.Specifications;
using CleanArq.Application.Users.Common.Dtos;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CleanArq.Application.Users.Commands.UpsertAddress;

public class UpsertAddressCommandHandler : IRequestHandler<UpsertAddressCommand, ErrorOr<AddressDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly string? _userEmail;

    public UpsertAddressCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _userEmail = httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
    }

    public async Task<ErrorOr<AddressDto>> Handle(UpsertAddressCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(_userEmail))
        {
            return Errors.User.AuthenticatedNotFound;
        }

        var spec = new UserWithAddressSpecification(_userEmail);

        var currentUser = await _unitOfWork.Repository<User>().GetAsync(spec);

        if (currentUser is not { } user)
        {
            return Errors.User.AuthenticatedNotFound;
        }

        if (user.Address is not { } address)
        {
            user.SetAddress(command.Street, command.City, command.Country);
        }
        else
        {
            address.UpdateAddress(command.Street, command.City, command.Country);
        }

        _unitOfWork.Repository<User>().Update(user);

        var result = await _unitOfWork.CompleteAsync();

        if (result == 0)
        {
            return Errors.User.SettingAddress;
        }

        return new AddressDto
        {
            Id = user.Address!.Id,
            Street = user.Address!.Street,
            City = user.Address!.City,
            Country = user.Address!.Country,
        };
    }
}
