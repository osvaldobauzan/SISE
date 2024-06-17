
using CJF.Sgcpj.Judicatura.Seguimiento.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Repositories;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddSeguimientoInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {


        services.AddLogging();
        
        services.AddTransient<ISeguimientoRepository, SeguimientoRepository>();
        return services;
    }





}
