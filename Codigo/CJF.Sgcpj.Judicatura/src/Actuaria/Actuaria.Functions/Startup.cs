using System;
using Actuaria.Functions;
using Azure.Identity;
using CJF.Sgcpj.Judicatura.Actuaria.Application;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarCOE.Acuse;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.SubirAcuse;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Actuario.GenerarAcuseOficio;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Actuario.GenerarAcuseOficio.Actuario;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Actuario.GenerarAcuseOficio.Fecha;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Procesos;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Actuaria.Infrastructure;
using CJF.Sgcpj.Judicatura.Actuaria.Infrastructure.Persistence.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Alertas;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Infrastructure;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Tramite.Infrastructure.Persistence.Repositories;
using Common.Functions;
using FluentValidation.AspNetCore;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Actuaria.Functions;

internal class Startup : FunctionsStartup
{
    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    {
        FunctionsHostBuilderContext context = builder.GetContext();
    }
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var configuration = new ConfigurationBuilder()
                           .SetBasePath(builder.GetContext().ApplicationRootPath)
                           .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                           .AddEnvironmentVariables()
                           .AddAzureAppConfiguration(options =>
                           {
                               options.Connect(Environment.GetEnvironmentVariable("AppConfigurationConnStr"))
                                       .ConfigureKeyVault(kv =>
                                       {
                                           kv.SetCredential(new DefaultAzureCredential());
                                       });
                           })
                           .Build();

        builder.Services.AddSingleton<IOpenApiConfigurationOptions>(_ =>
        {
            var options = new OpenApiConfigurationOptions()
            {
                Info = new OpenApiInfo()
                {
                    Version = "v1",
                    Title = $"API de Actuaría",
                    Description = "Esta API proporciona endpoints para gestionar los procesos de actuaría",
                    TermsOfService = new Uri("https://github.com/Azure/azure-functions-openapi-extension"),
                    Contact = new OpenApiContact()
                    {
                        Name = "Enquiry",
                        Email = "azfunc-openapi@microsoft.com",
                        Url = new Uri("https://github.com/Azure/azure-functions-openapi-extension/issues"),
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT"),
                    }
                },
                Servers = DefaultOpenApiConfigurationOptions.GetHostNames(),
                OpenApiVersion = Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums.OpenApiVersionType.V3,
                IncludeRequestingHostName = DefaultOpenApiConfigurationOptions.IsFunctionsRuntimeEnvironmentDevelopment(),
                ForceHttps = DefaultOpenApiConfigurationOptions.IsHttpsForced(),
                ForceHttp = DefaultOpenApiConfigurationOptions.IsHttpForced(),
            };

            return options;
        });

        builder.Services.AddSingleton<AzureADJwtBearerValidation>();
        builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
        builder.Services.AddScoped<IHttpRequestProcessor, HttpRequestProcessor>();
        builder.Services.AddScoped<ITramitesRepository, TramitesRepository>();
        builder.Services.AddScoped<IActuarioRepository, ActuarioRepository>();
        builder.Services.AddScoped<IGenerarAcuseOficioPorFechaService, GenerarAcuseOficioPorFechaService>();
        builder.Services.AddScoped<IGenerarAcuseOficioPorFechaService, GenerarAcuseOficioPorFechaService>();
        builder.Services.AddScoped<IRutasChunkService, RutasChunkService>();
        builder.Services.AddScoped<IGenerarAcuseOficioService, GenerarAcuseOficioService>();
        builder.Services.AddScoped<IServiceGeneracionOficio, ServiceGeneracionOficio>();
        builder.Services.AddScoped<ISubirAcuseComandoHandler, SubirAcuseComandoHandler>();
        builder.Services.AddScoped<IProcessQueueService, ProcessQueueService>();
        builder.Services.AddScoped<IGenerarAcuse, GenerarAcuse>();
        builder.Services.AddSingleton<Microsoft.Extensions.Configuration.IConfiguration>(configuration);
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddSingleton<IAlertsMessageService, AlertsMessageService>();

        builder.Services.AddApplicationServices();
        builder.Services.AddApplicationActuariaServices();
        builder.Services.AddInfrastructureActuariaServices(configuration);
        builder.Services.AddInfrastructureServices(configuration);
        builder.Services.AddFluentValidation(conf =>
        {
            conf.RegisterValidatorsFromAssembly(typeof(Startup).Assembly);
        });
    }
}
