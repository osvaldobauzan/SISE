using System.Text;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Common.Services;

[Collection("Services")]
public class WordContenidoTests : IAsyncLifetime
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
    public async void ReadDocumentWordShouldReturnOK()
    {
        var html = $"<div> Test </div>";
        var service = serviceProvider.GetService<IWordContenido>();

        var bytes = Encoding.UTF8.GetBytes(html);
        var result = service.ReadDocumentWord(bytes);

        result.Should().NotBeNull();
        result.Length.Should().BeGreaterThan(0);
    }

    [Fact]
    public async void ReplaceHTMLInWordBookmarkShouldReturnOK()
    {
        var html = $"<div> Test </div>";
        var service = serviceProvider.GetService<IWordContenido>();

        var bytes = Encoding.UTF8.GetBytes(html);
        var result = service.ReplaceHtmlInWordBookmark(bytes, $"<div> Replaceable HTML </div>");

        result.Should().NotBeNull();
        result.Length.Should().BeGreaterThan(0);
    }

    [Fact]
    public async void ReplaceHTMLInWordShouldReturnOK()
    {
        var html = $"<div> Test </div>";
        var service = serviceProvider.GetService<IWordContenido>();

        var bytes = Encoding.UTF8.GetBytes(html);
        var result = service.ReplaceHtmlInWord(bytes, $"<div> Replaceable HTML </div>");

        result.Should().NotBeNull();
        result.Length.Should().BeGreaterThan(0);
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IWordContenido, WordContenido>();
        services.AddScoped<ISanitizerService, SanitizerService>();
    }
}
