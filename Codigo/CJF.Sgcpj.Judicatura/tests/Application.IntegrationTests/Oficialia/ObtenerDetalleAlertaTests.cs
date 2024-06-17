using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Oficialia.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Oficialia;

[Collection(IntegrationTestsCollection.Name)]
public class ObtenerDetalleAlertaTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly ObtenerDetalleAlertaFunction? _sut;
    private readonly ILogger<ObtenerDetalleAlertaTests> _logger;

    public ObtenerDetalleAlertaTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<ObtenerDetalleAlertaFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<ObtenerDetalleAlertaTests>>();
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
            { "asuntoAlias", "1/2018" },
            { "modulo", "1" }
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
