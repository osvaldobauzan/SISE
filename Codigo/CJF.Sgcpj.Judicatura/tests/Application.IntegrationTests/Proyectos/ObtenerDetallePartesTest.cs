using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Proyectos.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Proyectos;

[Collection(IntegrationTestsCollection.Name)]
public class ObtenerDetallePartesTest
{
    private readonly TestsInitializer _testsInitializer;
    private readonly ObtenerDetallePartesFunction? _sut;
    private readonly ILogger<ObtenerDetallePartesTest> _logger;

    public ObtenerDetallePartesTest(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<ObtenerDetallePartesFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<ObtenerDetallePartesTest>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {

    }

    [Fact]
    public async void Get_ShouldRecoverDetallePartes()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("", new Dictionary<string, string>()
        {
            { "proyectoId", "1926" },
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
