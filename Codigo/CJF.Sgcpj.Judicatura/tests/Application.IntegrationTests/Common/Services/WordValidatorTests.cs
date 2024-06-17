using Azure.Identity;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Common.Services;

[Collection("Services")]
public class WordValidatorTests : IAsyncLifetime
{
    private IServiceProvider serviceProvider;

    public async Task DisposeAsync()
    {
    }

    public async Task InitializeAsync()
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        serviceProvider = serviceCollection.BuildServiceProvider();
    }

    [Fact]
    public async void IsDocXValidShouldReturnOK()
    {
        var service = serviceProvider.GetService<IWordValidator>();

        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Hola.docx");
        var bytes = File.ReadAllBytes(path);
        var result = service.isDocxValid(bytes);

        result.Should().BeTrue();
    }

    [Fact]
    public async void IsValidSignatureFromDocxShouldReturnOK()
    {
        var service = serviceProvider.GetService<IWordValidator>();

        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Hola.docx");
        var bytes = File.ReadAllBytes(path);
        var result = service.IsValidSignatureFromDocx("Hola.docx", bytes);

        result.Should().BeTrue();
    }

    [Fact]
    public async void IsNameAndValidatedExtensionShouldReturnOK()
    {
        var service = serviceProvider.GetService<IWordValidator>();
        var result = service.IsNameAndValidatedExtension("Hola.docx");

        result.Should().BeTrue();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IWordValidator, WordValidator>();
        services.AddScoped<IListSignatureFiles, ListSignatureFiles>();

        var configuration = new ConfigurationBuilder()
                   //.SetBasePath(builder.GetContext().ApplicationRootPath)
                   //.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
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

        services.AddSingleton<IConfiguration>(configuration);
    }
}
