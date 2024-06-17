using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tramites.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Tramites;

[Collection(IntegrationTestsCollection.Name)]
public class TramitesTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly TramitesFunction? _sut;
    private readonly ILogger<TramitesTests> _logger;

    public TramitesTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<TramitesFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<TramitesTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
    }

    [Fact]
    public async void Get_ShouldRecoverFiltrosTramiteOK()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("", new Dictionary<string, string>()
        {
            { "fechaInicial", "06/11/2023" },
            { "fechaFinal", "06/11/2023" },
            { "estado", "0" },
            { "ordenarPor", "promocion" },
            { "descendente", "true" },
            { "pagina", "1" },
            { "registrosPorPagina", "0" }
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


