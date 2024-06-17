using Azure.Identity;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Alertas;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Common.Services;

[Collection("Services")]
public class AlertsMessageServiceTests : IAsyncLifetime
{
    private IServiceCollection serviceCollection;
    private IServiceProvider serviceProvider;
    private bool injectFakeConfig;

    public async Task DisposeAsync()
    {
    }

    public async Task InitializeAsync()
    {
        serviceCollection = new ServiceCollection().AddLogging();
    }

    [Fact]
    public async void TriggerAlertShouldBeOk()
    {
        ConfigureServices(serviceCollection);
        serviceProvider = serviceCollection.BuildServiceProvider();

        var service = serviceProvider.GetService<IAlertsMessageService>();
        var result = await service.TriggerAlertAsync<SignalRAlertDTO>(new AlertDTO<SignalRAlertDTO>()
        {
            PersistirAlerta = false,
            TipoDeAlerta = AlertType.Test,
            Destinatarios = null,
            Alerta = null
        });

        result.IsSuccess.Should().BeTrue();
    }


    [Fact]
    public async void TriggerAlertShouldReturnFalseInCaseQueueUrlIsWrong()
    {
        injectFakeConfig = true;
        ConfigureServices(serviceCollection);
        serviceProvider = serviceCollection.BuildServiceProvider();

        var service = serviceProvider.GetService<IAlertsMessageService>();
        var result = await service.TriggerAlertAsync<SignalRAlertDTO>(new AlertDTO<SignalRAlertDTO>()
        {
            PersistirAlerta = false,
            TipoDeAlerta = AlertType.Test,
            Destinatarios = null,
            Alerta = null
        });

        result.IsSuccess.Should().BeFalse();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IAlertsMessageService, AlertsMessageService>();
        services.AddSingleton<Microsoft.Extensions.Logging.ILoggerFactory, NullLoggerFactory>();

        var configuration = new ConfigurationBuilder()
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

        if (injectFakeConfig)
        {
            configuration["SISE3:BackEnd:AlertasUrlCola"] = $"https://www.wrongurl.com";
        }

        services.AddSingleton<IConfiguration>(configuration);
    }
}
