using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Seguimiento.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Usuarios;

[Collection(IntegrationTestsCollection.Name)]
public class ObtenerSeguimientoXExpedienteTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly ObtenerSeguimientoXExpedienteFunction? _sut;
    private readonly ILogger<ObtenerSeguimientoXExpedienteTests> _logger;

    public ObtenerSeguimientoXExpedienteTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<ObtenerSeguimientoXExpedienteFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<ObtenerSeguimientoXExpedienteTests>>();
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
            { "Expediente", "2024/2024" }
        });

        // Act
        var response = await _sut!.Run(req.Object, _logger);
        var okResult = response as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
    }
}
