using System;
using Azure.Identity;
using CJF.Sgcpj.Judicatura.Common.Application;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Infrastructure;
using CJF.Sgcpj.Judicatura.Sentencias.Application;
using CJF.Sgcpj.Judicatura.Sentencias.Infrastructure;
using Common.Functions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Sentencias.Functions;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Sentencias.Functions;

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
                    Title = $"API de Sentencias",
                    Description = "Esta API proporciona endpoints para obtener las sentencias de un expediente",
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

        builder.Services.AddHttpClient("DocumentosHttpClient", client =>
        {
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        // Add services to the container.
        builder.Services.AddSingleton<AzureADJwtBearerValidation>();
        builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
        builder.Services.AddScoped<IHttpRequestProcessor, HttpRequestProcessor>();
        builder.Services.AddSingleton<IConfiguration>(configuration);
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddApplicationServices();
        builder.Services.AddApplicationSentenciasServices();
        builder.Services.AddInfrastructureServices(configuration);
        builder.Services.AddInfrastructureSentenciasServices(configuration);

        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssemblyContaining(typeof(Startup));
    }
}
