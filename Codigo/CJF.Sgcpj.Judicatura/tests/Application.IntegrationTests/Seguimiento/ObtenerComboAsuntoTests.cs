using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Seguimiento.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Seguimiento;

[Collection(IntegrationTestsCollection.Name)]
public class ObtenerComboAsuntoTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly ObtenerComboAsuntoFunction? _sut;
    private readonly ILogger<ObtenerComboAsuntoTests> _logger;

    public ObtenerComboAsuntoTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<ObtenerComboAsuntoFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<ObtenerComboAsuntoTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
    }

    [Fact]
    public async void GetCatalogoSecretarioShouldReturnOK()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("{}", new Dictionary<string, string>()
        {
            { "Expediente", "3388/2024" },
            { "TipoAsunto", "1" },
            { "TipoProcedimiento", "1" }
        });

        // Act
        var response = await _sut!.Run(req.Object, _logger);

        // Assert
        response.Should().NotBeNull();
    }
}
