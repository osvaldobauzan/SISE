using ExpedienteElectronico.Functions.Funciones;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.ExpedienteElectronico;

[Collection(IntegrationTestsCollection.Name)]
public class ObtenerParteTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly ObtenerParteFunction? _sut;
    private readonly ILogger<ObtenerParteTests> _logger;

    public ObtenerParteTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<ObtenerParteFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<ObtenerParteTests>>();
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
            { "personaId", "6712" }
        });

        // Act
        var response = await _sut!.Run(req.Object, _logger);
        var okResult = response as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    }
}
