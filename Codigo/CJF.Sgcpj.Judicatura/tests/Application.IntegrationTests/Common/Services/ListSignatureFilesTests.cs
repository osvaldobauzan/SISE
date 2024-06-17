using System.Text;
using Azure.Identity;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Common.Services;

[Collection("Services")]
public class ListSignatureFilesTests : IAsyncLifetime
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
    public async void InsertarQRTestShouldReturnOK()
    {
        var bytes = Encoding.UTF8.GetBytes("testByteArray");
        var service = serviceProvider.GetService<IListSignatureFiles>();
        var result = service.GetFilesSignaturesValid();

        result.Should().NotBeNull();
        result.Count.Should().BeGreaterThan(0);
    }

    private void ConfigureServices(IServiceCollection services)
    {
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
