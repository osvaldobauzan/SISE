using Actuaria.Functions.Funciones;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Catalogos;

[Collection(IntegrationTestsCollection.Name)]
public class DetallerAcuerdoTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly DetalleAcuerdoFunction? _sut;
    private readonly ILogger<DetallerAcuerdoTests> _logger;

    public DetallerAcuerdoTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<DetalleAcuerdoFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<DetallerAcuerdoTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
    }

    [Fact]
    public async void GET_ShouldReturnDetalleAcuerdo_WhenCalledWithValidData()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("{}", new Dictionary<string, string>()
        {
            { "asuntoNeunId", "30315408" },
            { "sintesisOrden", "1" },
            { "asuntoDocumentoId", "1" }
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
