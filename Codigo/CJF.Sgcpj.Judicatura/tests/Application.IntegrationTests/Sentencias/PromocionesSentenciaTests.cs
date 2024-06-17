using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sentencia.Functions.Funciones;
using Sentencias.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Sentencias;

[Collection(IntegrationTestsCollection.Name)]
public class PromocionesSentenciaTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly ObtenerPromocionesFunction? _sut;
    private readonly ILogger<PromocionesSentenciaTests> _logger;

    public PromocionesSentenciaTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<ObtenerPromocionesFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<PromocionesSentenciaTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {

    }

    [Fact]
    public async void Get_ShouldRecoverPromocionesSentenciaOK()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("", new Dictionary<string, string>()
        {
            { "asuntoId", "1" },
            { "asuntoNeunId", "30315014" },
            { "tipoCuaderno", "5645" },
            { "sintesisOrden", "1" },
        });

        // Act
        var response = await _sut!.Run(req.Object, _logger);
        var okResult = response as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
    }
}

