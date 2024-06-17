using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Proyectos.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Proyectos;

[Collection(IntegrationTestsCollection.Name)]
public class ObtenerCatalogosTests
{
    private readonly TestsInitializer _testsInitializer;
    private readonly ObtenerCatalogosFunction? _sut;
    private readonly ILogger<ObtenerCatalogosTests> _logger;

    public ObtenerCatalogosTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<ObtenerCatalogosFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<ObtenerCatalogosTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {

    }

    [Fact]
    public async void Get_ShouldOpcionesCatalogosProyecto()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("", new Dictionary<string, string>()
        {
            { "catTipoAsuntoId", "2" },
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
