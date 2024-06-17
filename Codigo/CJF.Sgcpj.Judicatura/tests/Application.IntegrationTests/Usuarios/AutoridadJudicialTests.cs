using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Usuarios.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Usuarios;

[Collection(IntegrationTestsCollection.Name)]
public class AutoridadJudicialTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly AutoridadJudicialFunction? _sut;
    private readonly ILogger<AutoridadJudicialTests> _logger;

    public AutoridadJudicialTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<AutoridadJudicialFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<AutoridadJudicialTests>>();
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
            { "Nombre", "Deyanira Flores Gomez" }
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
    public async void Get2ShouldReturnOK()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("{}", new Dictionary<string, string>()
        {
            { "AsuntoNeunId", "22239109" }
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
