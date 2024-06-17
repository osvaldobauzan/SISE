using Actuaria.Functions.Funciones;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarActuario;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Catalogos;

[Collection(IntegrationTestsCollection.Name)]
public class AgregarActuarioTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly AgregarActuarioFunction? _sut;
    private readonly ILogger<AgregarActuarioTests> _logger;

    public AgregarActuarioTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<AgregarActuarioFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<AgregarActuarioTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
    }

    [Fact]
    public async void Post_ShouldCreateActuario_WhenCalledWithValidData()
    {
        // Arrange
        var req = TestCommon.CreatePostMockRequest(new AgregarActuarioDto()
        {
            ActuarioId = 1,
            AsuntoId = 1,
            AsuntoNeunId = 1,
            Parte = 1
        });

        // Act
        var response = await _sut!.Run(req, _logger);
        var okResult = response as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        okResult.Value.Should().NotBeNull();


        // Act
        var response2 = await _sut!.Run2(req, _logger);
        var okResult2 = response2 as OkObjectResult;

        // Assert
        okResult2.Should().NotBeNull();
        okResult2.StatusCode.Should().Be(StatusCodes.Status200OK);
        okResult2.Value.Should().NotBeNull();
    }
}
