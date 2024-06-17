using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Seguimiento.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Seguimiento;

[Collection(IntegrationTestsCollection.Name)]
public class ObtenerSeguimientoConsultaXAliasAsuntoTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly ObtenerSeguimientoXAsuntoAliasTipoAsuntoFunction? _sut;
    private readonly ILogger<ObtenerSeguimientoConsultaXAliasAsuntoTests> _logger;

    public ObtenerSeguimientoConsultaXAliasAsuntoTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<ObtenerSeguimientoXAsuntoAliasTipoAsuntoFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<ObtenerSeguimientoConsultaXAliasAsuntoTests>>();
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
            { "Expediente", "2024/2024" },
            { "TipoAsunto", "1" },
            { "TipoProcedimiento", "1" }
        });

        // Act
        var response = await _sut!.Run(req.Object, _logger);
        var okResult = response as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        okResult.Value.Should().NotBeNull();
    }
}
