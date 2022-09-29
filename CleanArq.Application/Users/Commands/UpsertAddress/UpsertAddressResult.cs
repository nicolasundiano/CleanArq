using CleanArq.Application.Users.Common.Dtos;

namespace CleanArq.Application.Features.UserFeatures.Commands.UpsertAddress;

public record UpsertAddressResult(
    AddressDto Address);
