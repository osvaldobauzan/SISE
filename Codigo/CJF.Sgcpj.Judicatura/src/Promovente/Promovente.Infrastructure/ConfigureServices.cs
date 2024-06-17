using CJF.Sgcpj.Judicatura.Promovente.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Promovente.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CJF.Sgcpj.Judicatura.Promovente.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddPromoventeInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {


        services.AddTransient<IPromoventesRepository, PromoventesRepository>();       


        return services;
    }





}
