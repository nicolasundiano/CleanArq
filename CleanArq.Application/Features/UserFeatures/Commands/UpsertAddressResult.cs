using CleanArq.Application.Features.UserFeatures.Common;

namespace CleanArq.Application.Features.UserFeatures.Commands;

public record UpsertAddressResult(
    AddressDto Address);
