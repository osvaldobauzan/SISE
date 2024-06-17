using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CJF.Sgcpj.Judicatura.ExpedienteElectronico.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureExpedienteElectronicoServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IExpedienteElectronicoRepository, ExpedienteElectronicoRepository>();
        return services;
    }
}
