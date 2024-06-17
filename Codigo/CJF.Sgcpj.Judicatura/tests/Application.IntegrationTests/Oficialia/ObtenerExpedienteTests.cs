using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Oficialia.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Oficialia;

[Collection(IntegrationTestsCollection.Name)]
public class ObtenerExpedienteTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly ObtenerExpedienteFunction? _sut;
    private readonly ILogger<ObtenerExpedienteTests> _logger;

    public ObtenerExpedienteTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<ObtenerExpedienteFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<ObtenerExpedienteTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
    }

    [Fact]
    public async void Post_ObtenerBase64_ShouldReturnOk_WhenCalledWithValidData()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("{}", new Dictionary<string, string>()
        {
            { "asuntoNeunId", $"22239109" },
            { "anioPromocion", $"2024" },
            { "numeroOrden", $"0" },
            { "tipoModulo", $"1" },
            { "origen", $"1" },
            { "nombre", $"" },
            { "kIdElectronica", $"4" }
        });

        // Act
        var response = await _sut!.Run(req.Object, _logger);

        // Assert
        response.Should().NotBeNull();
    }

    //[Fact]
    //public async void Post_ObtenerArchivos_ShouldReturnOk_WhenCalledWithValidData()
    //{
    //    // Arrange
    //    var req = TestCommon.CreateMockRequest("{}", new Dictionary<string, string>()
    //    {
    //        { "ruta", "\\\\10.100.126.204\\desa_fs\\Promociones8\\180\\01800000303153030008230078040014.pdf" }
    //    });

    //    // Act
    //    var response = await _sut!.Run(req.Object, _logger);
    //    var okResult = response as OkObjectResult;

    //    // Assert
    //    okResult.Should().NotBeNull();
    //    okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    //    okResult.Value.Should().NotBeNull();
    //}
}
