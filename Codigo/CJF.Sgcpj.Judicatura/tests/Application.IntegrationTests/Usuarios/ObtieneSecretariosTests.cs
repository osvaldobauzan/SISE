using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Usuarios.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Usuarios;

[Collection(IntegrationTestsCollection.Name)]
public class ObtieneSecretariosTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly ObtieneSecretarioFunction? _sut;
    private readonly ILogger<ObtieneSecretariosTests> _logger;

    public ObtieneSecretariosTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<ObtieneSecretarioFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<ObtieneSecretariosTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
    }

    [Fact]
    public async void GetObtieneSecreatarioFunctionShouldReturnOKWithValidData()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("{}", new Dictionary<string, string>()
        {
            { "AsuntoNeunId", "30315408" }
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
