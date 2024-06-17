using CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Proyectos.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CJF.Sgcpj.Judicatura.Proyectos.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureProyectosServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IProyectosRepository, ProyectosRepository>();
        services.AddTransient<IArchivosRepository, ArchivosRepository>();

        return services;
    }
}
