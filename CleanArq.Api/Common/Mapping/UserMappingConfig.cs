using CleanArq.Application.Features.UserFeatures.Commands.UpsertAddress;
using CleanArq.Contracts.Authentication;
using CleanArq.Contracts.User;
using Mapster;

namespace CleanArq.Api.Common.Mapping;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpsertAddressRequest, UpsertAddressCommand>();

        config.NewConfig<UpsertAddressResult, UpsertAddressResponse>()
            .Map(dest => dest, src => src.Address);
    }
}
