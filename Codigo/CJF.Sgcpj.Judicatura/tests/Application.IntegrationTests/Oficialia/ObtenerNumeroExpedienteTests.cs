using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Oficialia.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Oficialia;

[Collection(IntegrationTestsCollection.Name)]
public class ObtenerNumeroExpedienteTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly ObtenerNumeroExpedienteFunction? _sut;
    private readonly ILogger<ObtenerNumeroExpedienteTests> _logger;

    public ObtenerNumeroExpedienteTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<ObtenerNumeroExpedienteFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<ObtenerNumeroExpedienteTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
    }

    [Fact]
    public async void Post_ShouldReturnOk_WhenCalledWithValidData()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("{}", new Dictionary<string, string>()
        {
            { "IdTipoAsunto", "1" },
            { "IdTipoProcedimiento", "1" }
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
