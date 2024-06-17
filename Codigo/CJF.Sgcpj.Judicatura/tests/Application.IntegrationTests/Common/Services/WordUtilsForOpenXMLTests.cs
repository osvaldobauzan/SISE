using System.Text;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Common.Services;

[Collection("Services")]
public class WordUtilsForOpenXMLTests : IAsyncLifetime
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
    public async void ReplaceTextInDocxShouldReturnOK()
    {
        var service = serviceProvider.GetService<IWordsUtil>();

        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Hola.docx");
        var bytes = File.ReadAllBytes(path);

        var result = service.ReplaceTextInDocx(bytes, "searchText", "replaceText");

        result.Should().NotBeNull();
        result.Length.Should().BeGreaterThan(1);
    }

    [Fact]
    public async void InsertQRCodeInWordDocumentShouldReturnOK()
    {
        var service = serviceProvider.GetService<IWordsUtil>();

        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Hola.docx");
        var bytes = File.ReadAllBytes(path);

        var result = service.InsertQRCodeInWordDocument(bytes, "qr1", "searchText", 100);

        result.Should().NotBeNull();
        result.Length.Should().BeGreaterThan(1);
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IWordsUtil, WordUtilsForOpenXML>();
        services.AddScoped<IGeneradorQR, GeneradorQRService>();
    }
}
