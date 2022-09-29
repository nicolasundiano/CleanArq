using CleanArq.Application.Users.Common.Dtos;
using ErrorOr;
using MediatR;

namespace CleanArq.Application.Users.Commands.UpsertAddress;

public class UpsertAddressCommand : IRequest<ErrorOr<AddressDto>>
{
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
}

