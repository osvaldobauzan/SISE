using System.Reflection;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Helpers;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Proyectos.Application.Common.Helpers;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationProyectosServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddSingleton<AlertsHelper>();
        services.AddSingleton<CryptographicHelper>();

        return services;
    }
}
