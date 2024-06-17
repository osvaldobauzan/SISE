using System;
using Agenda.Functions;
using Azure.Identity;
using CJF.Sgcpj.Judicatura.Agenda.Application;
using CJF.Sgcpj.Judicatura.Agenda.Infrastructure;
using CJF.Sgcpj.Judicatura.Common.Application;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Alertas;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Infrastructure;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
using Common.Functions;
using FluentValidation.AspNetCore;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Agenda.Functions
{
    internal class Startup: FunctionsStartup
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
                        Title = $"API de Agenda",
                        Description = "Esta API proporciona endpoints para gestionar la agenda de audiencias",
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
            builder.Services.AddScoped<IRutasChunkService, RutasChunkService>();
            builder.Services.AddSingleton<Microsoft.Extensions.Configuration.IConfiguration>(configuration);
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddSingleton<IAlertsMessageService, AlertsMessageService>();

            builder.Services.AddApplicationServices();
            builder.Services.AddApplicationAgendaServices();
            builder.Services.AddInfrastructureAgendaServices(configuration);
            builder.Services.AddInfrastructureServices(configuration);
            builder.Services.AddFluentValidation(conf =>
            {
                conf.RegisterValidatorsFromAssembly(typeof(Startup).Assembly);
            });
        }
    }
}
