using CleanArq.Application.Features.Authentication.Commands.Register;
using CleanArq.Application.Features.Authentication.Common;
using CleanArq.Application.Features.Authentication.Queries.Login;
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