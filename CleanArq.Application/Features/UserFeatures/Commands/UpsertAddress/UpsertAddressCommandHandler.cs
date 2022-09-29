using CleanArq.Application.Common.Interfaces.Persistence;
using CleanArq.Domain.Entities.User;
using CleanArq.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using CleanArq.Application.Features.UserFeatures.Common;
using CleanArq.Application.Features.UserFeatures.Common.Specifications;
using CleanArq.Application.Features.UserFeatures.Common.Dtos;

namespace CleanArq.Application.Features.UserFeatures.Commands.UpsertAddress;

public class UpsertAddressCommandHandler : IRequestHandler<UpsertAddressCommand, ErrorOr<UpsertAddressResult>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpsertAddressCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<UpsertAddressResult>> Handle(UpsertAddressCommand command, CancellationToken cancellationToken)
    {
        var spec = new UserWithAddressSpecification(command.UserEmail);

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

        return new UpsertAddressResult(
            new AddressDto
            {
                Id = user.Address!.Id,
                Street = user.Address!.Street,
                City = user.Address!.City,
                Country = user.Address!.Country,
            });
    }
}
