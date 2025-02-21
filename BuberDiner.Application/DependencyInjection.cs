using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BuberDiner.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services) 
    {
        var applicationAssembly = typeof(DependencyInjection).Assembly;
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
        return services;    
    }
}