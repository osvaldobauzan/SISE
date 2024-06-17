using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tramite.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Tramites;

[Collection(IntegrationTestsCollection.Name)]
public class FiltrosTramiteTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly FiltrosTramiteFunction? _sut;
    private readonly ILogger<FiltrosTramiteTests> _logger;

    public FiltrosTramiteTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<FiltrosTramiteFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<FiltrosTramiteTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
    }

    [Fact]
    public async void Get_ShouldRecoverFiltrosTramiteOK()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("");

        // Act
        var response = await _sut!.GetObtenerAcuerdo(req.Object, _logger);
        var okResult = response as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        okResult.Value.Should().NotBeNull();
    }
}


