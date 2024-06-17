using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sentencia.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Sentencias;

[Collection(IntegrationTestsCollection.Name)]
public class SentenciasTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly ObtenerSentenciasFunction? _sut;
    private readonly ILogger<SentenciasTests> _logger;

    public SentenciasTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<ObtenerSentenciasFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<SentenciasTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {

    }

    [Fact]
    public async void Get_ShouldRecoverFiltrosSentenciasOK()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("", new Dictionary<string, string>()
        {
            { "fecha", "02/04/2018" }
        });

        // Act
        var response = await _sut!.Run(req.Object, _logger);
        var okResult = response as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
    }
}
