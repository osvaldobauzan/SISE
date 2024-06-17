using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tramite.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Tramites;

[Collection(IntegrationTestsCollection.Name)]
public class AcuerdoTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly AcuerdoFunction? _sut;
    private readonly ILogger<AcuerdoTests> _logger;

    public AcuerdoTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<AcuerdoFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<AcuerdoTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
    }

    [Fact]
    public async void Get_ShouldRecoverBase64_WhenCalledWithValidData()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("{}", new Dictionary<string, string>()
        {
            { "id", "36301607-d6f8-4d89-8f7d-c52a4f168370" }
        });

        // Act
        var response = await _sut!.Run(req.Object, _logger);
        var okResult = response as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        okResult.Value.Should().NotBeNull();
    }

    [Fact]
    public async void Get_ShouldRecoverTramiteAcuerdo_WhenCalledWithValidData()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("{}", new Dictionary<string, string>()
        {
            { "asuntoNeunId", "30315021" },
            { "anioPromocion", "2023" },
            { "tipoOrden", "1091" },
            { "tipoModulo", "2" },
            { "asuntoDocumentoId", "1" }
        });

        // Act
        var response = await _sut!.Run2(req.Object, _logger);
        var okResult = response as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        okResult.Value.Should().NotBeNull();
    }

    [Fact]
    public async void Get_ShouldObtenerAcuerdoReturnsOk_WhenCalledWithValidData()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("{}", new Dictionary<string, string>()
        {
            { "idAsuntoNeun", "30314052" },
            { "ordenSintesis", "9" },
            { "asuntoDocumentoId", "12" }
        });

        // Act
        var response = await _sut!.GetObtenerAcuerdo(req.Object, _logger);
        var okResult = response as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        okResult.Value.Should().NotBeNull();
    }
}


