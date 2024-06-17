using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.WorkFlows;
using CJF.Sgcpj.Judicatura.Tramite.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CJF.Sgcpj.Judicatura.Tramite.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureTramitesServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ITramitesRepository, TramitesRepository>();
        services.AddTransient<IWorkflowsRepository, WorkFlowsRepository>();
        services.AddSingleton<IRecargarTemplatesService, RecargarTemplatesService>();
        return services;
    }
}
