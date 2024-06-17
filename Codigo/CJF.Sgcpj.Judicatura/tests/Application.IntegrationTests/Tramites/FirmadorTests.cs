using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tramite.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Tramites;

[Collection(IntegrationTestsCollection.Name)]
public class FirmadorTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;
    private readonly FirmadorFunction? _sut;
    private readonly ILogger<FirmadorTests> _logger;

    public FirmadorTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
        _sut = _testsInitializer.ServiceProvider.GetService<FirmadorFunction>();
        _logger = _testsInitializer.ServiceProvider.GetService<ILogger<FirmadorTests>>();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
    }

    [Fact]
    public async void Get_Should_Return_OK()
    {
        // Arrange
        var req = TestCommon.CreateMockRequest("{}", new Dictionary<string, string>()
        {
            { "id", "e751f1be-bb39-4b07-94b8-6da8c2178500" }
        });

        // Act
        var response = await _sut!.StatusFirmadorAsync(req.Object, _logger);

        // Assert
        response.Should().NotBeNull();
    }
}


