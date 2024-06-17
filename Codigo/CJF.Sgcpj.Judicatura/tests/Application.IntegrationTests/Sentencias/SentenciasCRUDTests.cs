using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sentencias.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Sentencias;

[Collection(IntegrationTestsCollection.Name)]
public class SentenciasCRUDTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly SentenciasFunction? _sut;
    private readonly ILogger<SentenciasCRUDTests> _logger;

    public SentenciasCRUDTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<SentenciasFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<SentenciasCRUDTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {

    }

    [Fact]
    public async void Get_ShouldCreateSentenciaOK()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("", new Dictionary<string, string>()
        {
            {"sentencia", "{\r\n  \"AsuntoNeunId\": 30314685,\r\n  \"AsuntoId\": 1,\r\n  \"NomArchivoReal\": \"Sentencia_UnitTest_28-31-2024.docx\",\r\n  \"TipoArchivo\": 3175,\r\n  \"Contenido\": 3969,\r\n  \"TipoCuadernoId\": 5645,\r\n  \"FechaAuto\": \"2024-05-20T00:00:00\",\r\n  \"Sigilo\": false,\r\n  \"SentenciaDefinitiva\": false,\r\n  \"EsJDA\": false,\r\n  \"TitularId\": 13885,\r\n  \"SecretarioPId\": 26459,\r\n  \"SecretarioCId\": 26459,\r\n  \"ActuarioId\": 0,\r\n  \"Resumen\": \"test123\",\r\n  \"VersionPublica\": 1,\r\n  \"InfoReservada\": 0,\r\n  \"Perspectiva\": 0,\r\n  \"Criterio\": 0,\r\n  \"Trascedental\": 0,\r\n  \"EsTratadoInternacional\": 0,\r\n  \"TipoActo\": 0,\r\n  \"NombreTratado\": 12208,\r\n  \"Derechos\": 12434,\r\n  \"DerechoEspecifico\": 12435,\r\n  \"TipoActoOtro\": null,\r\n  \"SolicitudReparacion\": 1,\r\n  \"SolicitudReparacionOpcion\": 2670,\r\n  \"SolicitudReparacionOtro\": \"otra repación.. a\",\r\n  \"LecturaFacil\": 0,\r\n  \"TemaEquidadGenero\": -1,\r\n  \"AplicacionEfectivaDerechoMujeres\": 0,\r\n  \"TemaAsuntosInternacionales\": 14511,\r\n  \"AplicaCriteriosPerspectivaGenero\": 0,\r\n  \"CriterioPerspectivaGeneroAplicado\": \"asd\",\r\n  \"Justificacion\": \"test\"\r\n}\r\n"},
            {"sentenciaVP", "{\r\n  \"AsuntoNeunId\": 30314685,\r\n  \"NumeroOrden\": 0,\r\n  \"SintesisOrden\": 0,\r\n  \"TipoOrigen\": 7,\r\n  \"DelincuenciaOrganizada\": false,\r\n  \"Confidencial\": false,\r\n  \"FraccionConfidencial\": \"16447\",\r\n  \"MotivacionConfidencial\": \"motivación de pruebas\",\r\n  \"ObservacionesConfidencial\": \"ninguna\",\r\n  \"UsuarioCaptura\": 57691,\r\n  \"Reservada\": false,\r\n  \"FraccionReservada\": null,\r\n  \"MotivacionReservada\": null,\r\n  \"ObservacionesReservada\": null\r\n}\r\n"}
        });

        // Act
        var response = await _sut!.Run(req.Object, _logger);
        var okResult = response as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
    }
}
