using CJF.Sgcpj.Judicatura.LibretaOficios.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.LibretaOficios.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CJF.Sgcpj.Judicatura.LibretaOficios.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureLibretaOficiossServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ILibretaOficiosRepository, LibretaOficiosRepository>();
        return services;
    }
}
