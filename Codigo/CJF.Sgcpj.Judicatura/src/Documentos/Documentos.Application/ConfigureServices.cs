using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Actuaria.Infrastructure.Persistence.Repositories;
using Documentos.Application.FirmadorDocumentos.Comandos;
using Documentos.Application.FirmadorDocumentos.Comandos.GenerarEvidenciaFirma;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Documentos.Application;
public static class ConfigureServices
{
    public static IServiceCollection AddApplicationDocumentosServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddTransient<IHojaFirmasService, HojaFirmasService > ();
        services.AddTransient<IActuariaRepository, ActuariaRepository>();

        return services;
    }
}
