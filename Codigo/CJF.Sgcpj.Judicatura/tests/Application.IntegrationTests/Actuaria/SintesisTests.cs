using Actuaria.Functions.Funciones;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.EditarSintesis;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.GuardarSintesis;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Catalogos;

[Collection(IntegrationTestsCollection.Name)]
public class SintesisTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly SintesisFunction? _sut;
    private readonly ILogger<SintesisTests> _logger;

    public SintesisTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<SintesisFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<SintesisTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
    }

    [Fact]
    public async void Post_ShouldCreateSintesis_WhenCalledWithValidDetails()
    {
        // Arrange
        var req = TestCommon.CreatePostMockRequest(new GuardarSintesisDto()
        {
            AsuntoDocumentoId = 1,
            AsuntoNeunId = 1,
            FechaAcuerdo = DateTime.UtcNow,
            NombreArchivo = "Nombre",
            Contenido = 1,
            ExtensionDocumento = ".docx",
            NombreDocumento = "NombreTest",
            Sintesis = "Sintesis",
            SintesisOrden = 1,
            TipoCuaderno = 1,
            UsuarioCaptura = 1
        });

        // Act
        var response = await _sut!.Run(req, _logger);

        // Assert
        response.Should().NotBeNull();
        var okResult = response as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        okResult.Value.Should().NotBeNull();
    }

    [Fact]
    public async void Put_ShouldEditSintesis_WhenCalledWithValidDetails()
    {
        // Arrange
        var req = TestCommon.CreatePostMockRequest(new EditarSintesisDto() 
        {
            AsuntoDocumentoId = 1,
            AsuntoNeunId = 2,
            FechaAcuerdo = DateTime.UtcNow,
            NombreArchivo = "NombreTest",
            Contenido = 1,
            ExtensionDocumento = "docx",
            NombreDocumento = "NombreTests",
            Sintesis = "Sintesis",
            SintesisOrden = 1,
            TipoCuaderno = 1,
            UsuarioCaptura = 1
        });

        // Act
        var response = await _sut!.Run2(req, _logger);
        var okResult = response as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        okResult.Value.Should().NotBeNull();
    }
}
