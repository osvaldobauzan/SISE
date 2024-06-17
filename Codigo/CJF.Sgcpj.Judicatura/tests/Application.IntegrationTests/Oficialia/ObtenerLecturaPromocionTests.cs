using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Oficialia.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Oficialia;

[Collection(IntegrationTestsCollection.Name)]
public class ObtenerLecturaPromocionTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly ObtenerLecturaPromocionFunction? _sut;
    private readonly ILogger<ObtenerLecturaPromocionTests> _logger;

    public ObtenerLecturaPromocionTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<ObtenerLecturaPromocionFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<ObtenerLecturaPromocionTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
    }

    [Fact]
    public async void Post_ShouldReturnOk_WhenCalledWithValidData()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("{}", new Dictionary<string, string>()
        {
            { "asuntoNeunId", "30315209" },
            { "asuntoID", "2" },
            { "YearPromocion", "2023"},
            { "NumeroOrden", "759" },
            { "CatIdOrganismo", "180" },
            { "NumeroRegistro", "7743" },
            { "OrigenPromocion", "4" },
            { "TipoModulo", "2" }
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
