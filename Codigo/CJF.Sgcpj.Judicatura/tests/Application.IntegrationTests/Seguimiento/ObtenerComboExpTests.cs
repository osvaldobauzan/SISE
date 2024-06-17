using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Seguimiento.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Seguimiento;

[Collection(IntegrationTestsCollection.Name)]
public class ObtenerComboExpTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly ObtenerSeguimientoComboExpFunction? _sut;
    private readonly ILogger<ObtenerComboExpTests> _logger;

    public ObtenerComboExpTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<ObtenerSeguimientoComboExpFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<ObtenerComboExpTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
    }

    [Fact]
    public async void Get_Should_ReturnOK()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("{}", new Dictionary<string, string>()
        {
            { "Expediente", "2024/2024" },
            { "TipoDocumento", "1" }
        });

        // Act
        var response = await _sut!.Run(req.Object, _logger);
        var okResult = response as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    }
}
