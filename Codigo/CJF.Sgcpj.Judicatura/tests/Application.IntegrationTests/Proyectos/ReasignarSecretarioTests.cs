using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Proyectos.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Proyectos;

public class ReasignarSecretarioTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly ReasignarSecretarioFunction? _sut;
    private readonly ILogger<ReasignarSecretarioTests> _logger;

    public ReasignarSecretarioTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<ReasignarSecretarioFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<ReasignarSecretarioTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public Task InitializeAsync()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public async void Get_ShouldValidateExpedienteOK()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("", new Dictionary<string, string>()
        {
            { "SecretarioNuevoId", "34556" },
            { "ProyectosId", "[2022]" }
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
