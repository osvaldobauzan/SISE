using Actuaria.Functions.Funciones;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Catalogos;

[Collection(IntegrationTestsCollection.Name)]
public class NotificacionesTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly NotificacionesFunction? _sut;
    private readonly ILogger<NotificacionesTests> _logger;

    public NotificacionesTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<NotificacionesFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<NotificacionesTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
    }

    [Fact]
    public async void GetNotificationsShouldReturnOKWithValidNoteDetails()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("{}", new Dictionary<string, string>()
        {
            { "tipoOrden", "true" },
            { "filtroTipo", "0" },
            { "TamanioPagina", "0" },
            { "NumeroPagina", "1" },
            { "AsuntoNeunId", "30315607" },
            { "AsuntoDocumentoID", "1" }
        });

        // Act
        var response = await _sut!.Run(req.Object, _logger);
        var okResult = response as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        okResult.Value.Should().NotBeNull();
    }

    [Fact]
    public async void PostNotificationAcuseShouldReturnOKWithValidNoteDetails()
    {
        // Arrange
        var req = TestCommon.CreatePostFormCollectionMockRequest(new Dictionary<string, string>()
        {
            { "fechaNotificacionCitatorio", "11/11/2023" },
            { "fechaNotificacion", "02/02/2024" },
            { "sintesisOrden", "0" },
            { "tipoAcuse", "1" },
            { "asuntoNeunId", "30315607" },
            { "sintesisCitatorio", "1" }
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
