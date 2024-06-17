using FluentAssertions;
using LibretaOficios.Functions.Funciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Catalogos;

[Collection(IntegrationTestsCollection.Name)]
public class ObtenerLibretaOficioTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly ObtenerLibretaOficioFunction? _sut;
    private readonly ILogger<ObtenerLibretaOficioTests> _logger;

    public ObtenerLibretaOficioTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<ObtenerLibretaOficioFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<ObtenerLibretaOficioTests>>();
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
            { "folio", "658" },
            { "asuntoNeunId", "30315209" },
            { "Anio", "2023" },
            { "pagina", "1" },
            { "registrosPorPagina", "1" }
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
