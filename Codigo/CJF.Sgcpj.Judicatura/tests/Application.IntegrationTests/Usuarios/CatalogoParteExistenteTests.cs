using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Usuarios.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Usuarios;

[Collection(IntegrationTestsCollection.Name)]
public class CatalogoParteExistenteTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly CatalogoParteExistenteFunction? _sut;
    private readonly ILogger<CatalogoParteExistenteTests> _logger;

    public CatalogoParteExistenteTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<CatalogoParteExistenteFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<CatalogoParteExistenteTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
    }

    [Fact]
    public async void GetCatalogoSecretarioShouldReturnOK()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("{}", new Dictionary<string, string>()
        {
            { "AsuntoNeunId", "30315408" },
            { "Modulo", "1" },
            { "TipoParte", "1" }
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
