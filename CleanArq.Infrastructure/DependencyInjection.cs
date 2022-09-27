using CleanArq.Infrastructure.Persistence;
using CleanArq.Infrastructure.Persistence.Repositories;
using CleanArq.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArq.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

        services.AddDbContext<AppDbContext>(c =>
                c.UseSqlServer(configuration.GetConnectionString("AppConnection")));

        return services;
    }
}
