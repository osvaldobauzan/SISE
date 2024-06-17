using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Proyectos.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Proyectos;

[Collection(IntegrationTestsCollection.Name)]
public class VersionesProyectoTests
{
    private readonly TestsInitializer _testsInitializer;
    private readonly ObtenerVersionesProyectoFunction? _sut;
    private readonly ILogger<VersionesProyectoTests> _logger;

    public VersionesProyectoTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<ObtenerVersionesProyectoFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<VersionesProyectoTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {

    }

    [Fact]
    public async void Get_ShouldRecoverVersionesProyecto()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("", new Dictionary<string, string>()
        {
            { "asuntoNeunId", "30315014" },
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
    public async void Get_ShouldRecoverUltimaVersionProyecto()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("", new Dictionary<string, string>()
        {
            { "asuntoNeunId", "30315014" },
        });

        // Act
        var response = await _sut!.Run2(req.Object, _logger);
        var okResult = response as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        okResult.Value.Should().NotBeNull();
    }
}
