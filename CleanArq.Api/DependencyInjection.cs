using CleanArq.Api.Common.Errors;
using CleanArq.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CleanArq.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddHttpContextAccessor();
        services.AddSingleton<ProblemDetailsFactory, CleanArqProblemDetailsFactory>();
        services.AddMappings();

        return services;
    }
}
