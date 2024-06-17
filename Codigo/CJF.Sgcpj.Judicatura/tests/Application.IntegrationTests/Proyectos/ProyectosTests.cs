using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Proyectos.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Proyectos;

[Collection(IntegrationTestsCollection.Name)]
public class ProyectosTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly ProyectosFunction? _sut;
    private readonly ILogger<ProyectosTests> _logger;
    private string _fileDocx;

    public ProyectosTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<ProyectosFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<ProyectosTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
        _fileDocx = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Hola.docx");
    }

    [Fact]
    public async void Get_ShouldRecoverFiltrosProyectosOK()
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
    }

    //[Fact]
    //public async void Get_ShouldUploadFileProyectoOK()
    //{
    //    //Arrange
    //    var req = TestCommon.CreatePostFormCollectionMockRequest(
    //        //new Dictionary<string, string>()
    //        //{
    //        //    { "Assets/Hola.docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" }
    //        //},
    //        new Dictionary<string, string>()
    //        {
    //            { "catOrganismoId", "180" },
    //            { "asuntoNeunId", "30315376" },
    //            { "titularId", "34556" },
    //            { "secretarioId", "45712" },
    //            { "tipoSentenciaId", "1" },
    //            { "sentidoId", "2" },
    //            { "sintesis", "test123" },
    //            { "motivosPartes", "[\r\n  {\r\n    \"IdParte\": 987654321,\r\n    \"IdMotivo\": 333,\r\n    \"IdSentido\": 4444,\r\n    \"Descripcion\": \"Test1234567890\"\r\n  },\r\n  {\r\n    \"IdParte\": 123456789,\r\n    \"IdMotivo\": 1,\r\n    \"IdSentido\": 22,\r\n    \"Descripcion\": \"Prueba1234567890\"\r\n  }\r\n]" }
    //        });

    //    // Act
    //    var response = await _sut!.Run2(req, _logger);
    //    var okResult = response as OkObjectResult;

    //    // Assert
    //    okResult.Should().NotBeNull();
    //    okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    //    okResult.Value.Should().NotBeNull();
    //}

    [Fact]
    public async void Get_ShouldValidateExpedienteOK()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("", new Dictionary<string, string>()
        {
            { "cuadernoId", "1" },
            { "asuntoNeunId", "30315376" }
        });

        // Act
        var response = await _sut!.Run3(req.Object, _logger);
        var okResult = response as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        okResult.Value.Should().NotBeNull();
    }
}
