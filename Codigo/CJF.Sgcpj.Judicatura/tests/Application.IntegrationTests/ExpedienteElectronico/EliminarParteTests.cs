using ExpedienteElectronico.Application.Common.Models;
using ExpedienteElectronico.Functions.Funciones;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.ExpedienteElectronico;

[Collection(IntegrationTestsCollection.Name)]
public class EliminarParteTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly EliminarParteFunction? _sut;
    private readonly ILogger<EliminarParteTests> _logger;

    public EliminarParteTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<EliminarParteFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<EliminarParteTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
    }

    [Fact]
    public async void PostShouldReturnOK()
    {
        // Arrange
        var req = TestCommon.CreatePostMockRequest(new PersonaAsuntoDelete()
        {
            PersonaId = 1,
            UsuarioElimina = 1
        });

        // Act
        var response = await _sut!.Run(req, _logger);
        var okResult = response as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        okResult.Value.Should().NotBeNull();
    }
}
