using CleanArq.Application.Users.Common.Dtos;
using ErrorOr;
using MediatR;

namespace CleanArq.Application.Users.Commands.UpsertAddress;

public record UpsertAddressCommand(
     string? Street,
     string? City,
     string? Country) : IRequest<ErrorOr<AddressDto>>;

     


