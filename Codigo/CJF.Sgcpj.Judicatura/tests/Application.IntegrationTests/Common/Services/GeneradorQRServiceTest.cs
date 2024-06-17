using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Common.Services;

[Collection("Services")]
public class GeneradorQRServiceTest : IAsyncLifetime
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
    public async void GenerarQRToBase64ShouldReturnOK()
    {
        var service = serviceProvider.GetService<IGeneradorQR>();
        var result = service.GenerarQrToBase64("textToEncode", 100);

        result.Should().NotBeNull();
        result.Length.Should().BeGreaterThan(1);
    }

    [Fact]
    public async void GenerarQRWithLogoShouldReturnOK()
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "test.png");
        var imageBytes = File.ReadAllBytes(path);

        var service = serviceProvider.GetService<IGeneradorQR>();
        var result = service.GenerarQRWithLogo("textToEncode", 100, imageBytes);

        result.Should().NotBeNull();
        result.Length.Should().BeGreaterThan(1);
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IGeneradorQR, GeneradorQRService>();
    }
}
