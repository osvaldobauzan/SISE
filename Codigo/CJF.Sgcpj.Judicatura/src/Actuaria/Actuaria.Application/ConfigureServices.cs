using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationActuariaServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
}
