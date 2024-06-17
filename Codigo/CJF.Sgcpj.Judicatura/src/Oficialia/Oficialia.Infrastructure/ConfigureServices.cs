using CJF.Sgcpj.Judicatura.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Oficialia.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CJF.Sgcpj.Judicatura.Oficialia.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddOficialiaInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLogging();

        services.AddSingleton<IRecargarTemplatesService, RecargarTemplatesService>();
        services.AddTransient<IPromocionesRepository, PromocionesRepository>();
        services.AddTransient<IOficialiaPartesRepository, OficialiaPartesRepository>();
        return services;
    }
}
