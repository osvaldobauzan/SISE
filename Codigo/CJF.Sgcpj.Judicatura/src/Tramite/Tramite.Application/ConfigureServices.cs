using System.Reflection;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Bre;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CJF.Sgcpj.Judicatura.Tramite.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationTramitesServices(this IServiceCollection services)
    {
        services.AddTransient<BreHelper>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


        return services;
    }
}
