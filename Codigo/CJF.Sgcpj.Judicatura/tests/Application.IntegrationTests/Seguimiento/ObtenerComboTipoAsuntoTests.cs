using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Seguimiento.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Seguimiento;

[Collection(IntegrationTestsCollection.Name)]
public class ObtenerComboTipoAsuntoTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly ObtenerComboTipoAsuntoFunction? _sut;
    private readonly ILogger<ObtenerComboTipoAsuntoTests> _logger;

    public ObtenerComboTipoAsuntoTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<ObtenerComboTipoAsuntoFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<ObtenerComboTipoAsuntoTests>>();
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
        });

        // Act
        var response = await _sut!.Run(req.Object, _logger);
        var okResult = response as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    }
}
