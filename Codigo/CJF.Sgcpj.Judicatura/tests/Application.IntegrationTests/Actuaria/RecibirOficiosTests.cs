using Actuaria.Functions.Funciones;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.RecibirOficios;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Catalogos;

[Collection(IntegrationTestsCollection.Name)]
public class RecibirOficiosTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly RecibirOficiosFunction? _sut;
    private readonly ILogger<RecibirOficiosTests> _logger;

    public RecibirOficiosTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<RecibirOficiosFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<RecibirOficiosTests>>();
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
            { "folio", "520" }
        });

        // Act
        var response = await _sut!.Run(req.Object, _logger);

        // Assert
        response.Should().NotBeNull();
    }

    [Fact]
    public async void Post_ShouldCreateActuariaOficios_WhenCalledWithValidNoteDetails()
    {
        // Arrange
        var req = TestCommon.CreatePostMockRequest(new List<RecibirOficiosDto>() 
        {
            new RecibirOficiosDto()
            {
                AnexoId = 1,
                AsuntoNeunId = 1,
                CatTipoAsuntoId = 1,
                ConArchivo = 1,
                TipoAsuntoDescripcion = "test",
                CatOrganismoId = 1,
                Expediente = "tests",
                Folio = 1,
                NombreTipoCuaderno = "test",
                Recibido = true,
                TipoCuaderno = 2,
                TipoNotificacion = "test"
            } 
        });

        // Act
        var response = await _sut!.Post(req, _logger);
        var okResult = response as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        okResult.Value.Should().NotBeNull();
    }
}
