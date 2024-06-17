using ExpedienteElectronico.Functions.Funciones;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.ExpedienteElectronico;

[Collection(IntegrationTestsCollection.Name)]
public class ObtenerFichaTecnicaExpedienteElectronicoTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly ObtenerFichaTecnicaExpedienteElectronicoFunction? _sut;
    private readonly ILogger<ObtenerFichaTecnicaExpedienteElectronicoTests> _logger;

    public ObtenerFichaTecnicaExpedienteElectronicoTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<ObtenerFichaTecnicaExpedienteElectronicoFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<ObtenerFichaTecnicaExpedienteElectronicoTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
    }

    [Fact]
    public async void GetShouldReturnOK()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("{}", new Dictionary<string, string>()
        {
            { "asuntoNeunId", "30315607" }
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
