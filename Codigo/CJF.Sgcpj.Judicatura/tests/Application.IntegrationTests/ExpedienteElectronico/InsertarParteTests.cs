using ExpedienteElectronico.Application.Common.Models;
using ExpedienteElectronico.Functions.Funciones;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.ExpedienteElectronico;

[Collection(IntegrationTestsCollection.Name)]
public class InsertarParteTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly InsertarParteFunction? _sut;
    private readonly ILogger<InsertarParteTests> _logger;

    public InsertarParteTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<InsertarParteFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<InsertarParteTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
    }

    [Fact]
    public async void PostShouldReturnOK()
    {
        // Arrange
        var req = TestCommon.CreatePostMockRequest(new PersonaAsuntoInsert()
        {
            AsuntoNeunId = 1,
            IdOrganoPlenos = 1,
            PersonaId = 1,
            UsuarioCaptura = 1,
            PersonaAsunto = new PersonaAsuntoDTO()
            {
                AceptaOponePublicarDatos = 1,
                Alias = "Alias",
                AMaterno = "AMaterno",
                APaterno = "APaterno",
                CatCaracterPersonaAsuntoId = 1,
                ClasificaAutoridadGenericaId = 1,
                DenominacionDeAutoridad = "Denominacion",
                FechaAceptaOponePublicarDatos = "02/02/2023",
                ParteAdhesivaApelacion = 1,
                SujetoDerechoAgrario = 1,
                CaracterPromueveNombre = 1,
                CatTipoPersonaId = 1,
                CatTipoPersonaJuridicaId = 1,
                EdadMenor = 1,
                EsParteGrupoVulnerable = 1,
                Foraneo = 1,
                GrupoVulnerable = 1,
                HablaLengua = 1,
                Lengua = 1,
                MayorEdad = 1,
                Nombre = "Nombre",
                Recurrente = 1,
                Sexo = 1,
                Traductor = 1,
                VictimaOfendidoDelito = 1
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
