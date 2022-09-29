using CleanArq.Application.Features.UserFeatures.Authentication.Commands.Register;
using CleanArq.Application.Features.UserFeatures.Authentication.Common;
using CleanArq.Application.Features.UserFeatures.Authentication.Queries.Login;
using CleanArq.Contracts.Authentication;
using Mapster;

namespace CleanArq.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();

        config.NewConfig<LoginRequest, LoginQuery>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest, src => src.User);
    }
}