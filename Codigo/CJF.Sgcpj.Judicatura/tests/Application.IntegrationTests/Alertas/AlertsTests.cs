using Actuaria.Functions.Funciones;
using Alertas.Functions.Funciones;
using CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Comandos.RecuperarAlertas;
using CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Catalogos;

[Collection(IntegrationTestsCollection.Name)]
public class AlertsTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly AlertsFunction? _sut;
    private readonly ILogger<RecuperarAlertasCommandHandler> _logger;

    public AlertsTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<AlertsFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<RecuperarAlertasCommandHandler>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
    }

    [Fact]
    public async void Post_ShouldCreateNote_WhenCalledWithValidNoteDetails()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("{}");

        // Act
        var response = await _sut!.GetAlertsByUserIdAsync(req.Object);
        var okResult = response as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        okResult.Value.Should().NotBeNull();
    }

    [Fact]
    public async void Post_ShouldHandleconnections_WhenCalledWithValidData()
    {
        // Arrange
        var req = TestCommon.CreatePostMockRequest(new UserConnectionsDTO()
        {
            PastConnection = "PastConnectionTest", 
            CurrentConnection = "CurrentConnectionTest"
        });

        // Act
        var response = await _sut!.HandleConnectionsAsync(req);
        var okResult = response as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        okResult.Value.Should().NotBeNull();
    }
}
