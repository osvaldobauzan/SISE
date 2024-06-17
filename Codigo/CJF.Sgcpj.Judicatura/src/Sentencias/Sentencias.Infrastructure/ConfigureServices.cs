using CJF.Sgcpj.Judicatura.Sentencias.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Sentencias.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CJF.Sgcpj.Judicatura.Sentencias.Infrastructure.Persistence.Repositories;

namespace CJF.Sgcpj.Judicatura.Sentencias.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureSentenciasServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ISentenciasRepository, SentenciasRepository>();
        services.AddTransient<IArchivosRepository, ArchivosRepository>();
        services.AddTransient<IPromocionesRepository, PromocionesRepository>();

        return services;
    }
}
