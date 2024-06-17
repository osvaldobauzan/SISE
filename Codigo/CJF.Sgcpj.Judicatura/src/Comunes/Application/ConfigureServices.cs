using System;
using System.Reflection;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Behaviours;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Seguridad.Application.Common.Helpers;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CJF.Sgcpj.Judicatura.Common.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.AddSingleton<PrivilegiosSISE3>();
        return services;
    }
}
