using Actuaria.Functions.Funciones;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarActuario;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarActuarioMasivo;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Catalogos;

[Collection(IntegrationTestsCollection.Name)]
public class AgregarActuarioMasivoTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly AgregarActuarioMasivoFunction? _sut;
    private readonly ILogger<AgregarActuarioMasivoTests> _logger;

    public AgregarActuarioMasivoTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<AgregarActuarioMasivoFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<AgregarActuarioMasivoTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
    }

    [Fact]
    public async void Post_ShouldCreateActuarioMasivo_WhenCalledWithValidData()
    {
        // Arrange
        var req = TestCommon.CreatePostMockRequest(new AgregarActuarioMasivoDto()
        {
            ActuarioId = 1,
            AsuntoNeunId = 1,
            SintesisOrden = 1,
            PartesNotificaciones = new List<ParteNotificacionDto>()
            {
                new ParteNotificacionDto()
                {
                    ParteId = 1,
                    TieneCOE = true,
                    TipoNotificacionID = 1
                }
            }
        });

        // Act
        var response = await _sut!.Run(req, _logger);
        var okResult = response as OkObjectResult;

        // Assert
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
        okResult.Value.Should().NotBeNull();
    }
}
