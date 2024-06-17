using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Oficialia.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Seguimiento;

[Collection(IntegrationTestsCollection.Name)]
public class ObtenerSeguimientoTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly ObtenerSeguimientoFunction? _sut;
    private readonly ILogger<ObtenerSeguimientoTests> _logger;

    public ObtenerSeguimientoTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<ObtenerSeguimientoFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<ObtenerSeguimientoTests>>();
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
            { "FechaIni", DateTime.UtcNow.AddMonths(-3).ToShortDateString() },
            { "FechaFin", DateTime.UtcNow.AddMonths(-2).ToShortDateString() }
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
