using Application.Interfaces;
using Application.Services;
using Core.Interfaces;
using infra.data.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class InfrastructureServicesExtension
{
    public static IServiceCollection AddDbContextServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("ApplicationDatabase"));
        });
        return serviceCollection;
    }

    public static IServiceCollection AddDependencyInjectionServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

        return serviceCollection;
    }
}
