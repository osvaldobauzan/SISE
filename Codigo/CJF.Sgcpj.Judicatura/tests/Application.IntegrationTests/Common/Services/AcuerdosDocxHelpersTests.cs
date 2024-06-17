using System.Text;
using Azure.Identity;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Common.Services;

[Collection("Services")]
public class AcuerdosDocxHelpersTests : IAsyncLifetime
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
        var service = serviceProvider.GetService<IAcuerdosDocxsHelpers>();
        var result = service.InsertarQrLateral(bytes, "textQrCodeText", 100);

        result.Should().NotBeNull();
    }


    [Fact]
    public async void InsertarQRTestShouldThrowValidationErrors()
    {
        var service = serviceProvider.GetService<IAcuerdosDocxsHelpers>();

        try
        {
            service.InsertarQrLateral(null, "qrCodeText", 100);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<Exception>();
        }


        var bytes = Encoding.UTF8.GetBytes("testByteArray");

        try
        {
            service.InsertarQrLateral(bytes, null, 100);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<ArgumentNullException>();
        }

        try
        {
            service.InsertarQrLateral(bytes, "qrCodeText", 1);
        }
        catch (Exception ex)
        {
            ex.Should().BeOfType<Exception>();
        }
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IAcuerdosDocxsHelpers, AcuerdosDocxsHelpers>();
        services.AddScoped<IGeneradorQR, GeneradorQRService>();
        services.AddScoped<AsposeLicense>();


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
