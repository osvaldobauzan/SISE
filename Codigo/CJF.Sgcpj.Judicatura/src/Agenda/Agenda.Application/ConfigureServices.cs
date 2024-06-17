﻿using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CJF.Sgcpj.Judicatura.Agenda.Application;
public static class ConfigureServices
{
    public static IServiceCollection AddApplicationAgendaServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
}
