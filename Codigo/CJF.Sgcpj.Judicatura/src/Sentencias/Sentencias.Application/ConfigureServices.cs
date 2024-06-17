using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CJF.Sgcpj.Judicatura.Sentencias.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationSentenciasServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
