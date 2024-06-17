using CJF.Sgcpj.Judicatura.Seguridad.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Seguridad.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CJF.Sgcpj.Judicatura.Seguridad.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureSeguridadServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLogging();


        services.AddTransient<ISeguridadRepository, SeguridadRepository>();
        return services;
    }





}
