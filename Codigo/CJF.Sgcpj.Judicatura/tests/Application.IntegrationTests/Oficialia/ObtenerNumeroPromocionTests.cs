using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Oficialia.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Oficialia;

[Collection(IntegrationTestsCollection.Name)]
public class ObtenerNumeroPromocionTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly ObtenerNumeroPromocionFunction? _sut;
    private readonly ILogger<ObtenerNumeroPromocionTests> _logger;

    public ObtenerNumeroPromocionTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<ObtenerNumeroPromocionFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<ObtenerNumeroPromocionTests>>();
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
            { "NumeroRegistro", "7802" },
            { "YearPromocion", "2023" }
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
