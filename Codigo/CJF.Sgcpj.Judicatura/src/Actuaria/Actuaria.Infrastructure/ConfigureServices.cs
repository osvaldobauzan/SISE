using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Actuaria.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CJF.Sgcpj.Judicatura.Actuaria.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureActuariaServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IActuariaRepository, ActuariaRepository>();
        return services;
    }
}