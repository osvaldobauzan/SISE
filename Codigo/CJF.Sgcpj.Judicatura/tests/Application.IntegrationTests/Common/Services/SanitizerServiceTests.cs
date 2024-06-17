using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Common.Services;

[Collection("Services")]
public class SanitizerServiceTests : IAsyncLifetime
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
    public async void SanitizeHTMLShouldReturnOK()
    {
        var html = $"<div> Test <script>document.getElementById('dummy');<script/></div>";
        var service = serviceProvider.GetService<ISanitizerService>();
        var result = service.SanitizeHtml(html);

        result.Should().NotBeNull();
        result.Length.Should().BeGreaterThan(0);
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<ISanitizerService, SanitizerService>();
    }
}
