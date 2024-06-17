using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using Catalogos.Functions.Funciones;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using Newtonsoft.Json;
using Moq;
using Microsoft.Extensions.Primitives;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Catalogos;

[Collection(IntegrationTestsCollection.Name)]
public class CatalogoTipoPersonaTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly CatalogoTipoPersonaFunction? _sut;
    private readonly ILogger<CatalogoTipoPersonaTests> _logger;

    public CatalogoTipoPersonaTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<CatalogoTipoPersonaFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<CatalogoTipoPersonaTests>>();
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
        var req = TestCommon.CreateMockRequest("{}", new Dictionary<string, string>()
        {
            { "TipoAsuntoId", "1" }
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
