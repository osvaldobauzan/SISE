using Catalogos.Functions.Funciones;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Catalogos;

[Collection(IntegrationTestsCollection.Name)]
public class CatalogoTipoPersonaJuridicaTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly CatalogoTipoPersonaJuridicaFunction? _sut;
    private readonly ILogger<CatalogoTipoPersonaJuridicaTests> _logger;

    public CatalogoTipoPersonaJuridicaTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<CatalogoTipoPersonaJuridicaFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<CatalogoTipoPersonaJuridicaTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
    }

    [Fact]
    public async void Get_ShouldReturn_OK()
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
