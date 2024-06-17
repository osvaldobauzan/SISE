using Actuaria.Functions.Funciones;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Catalogos;

[Collection(IntegrationTestsCollection.Name)]
public class ActuariaTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly ActuariaFunction? _sut;
    private readonly ILogger<ActuariaTests> _logger;

    public ActuariaTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<ActuariaFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<ActuariaTests>>();
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
            { "fechaInicial", "06/11/2023" },
            { "fechaFinal", "06/11/2023" },
            { "tipoFiltro", "1" },
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
