using CJF.Sgcpj.Judicatura.Agenda.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Agenda.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CJF.Sgcpj.Judicatura.Agenda.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureAgendaServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IAgendaRepository, AgendaRepository>();
        return services;
    }
}