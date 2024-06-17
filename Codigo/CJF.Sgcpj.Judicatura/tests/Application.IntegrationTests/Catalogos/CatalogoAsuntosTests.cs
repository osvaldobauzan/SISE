using Catalogos.Functions.Funciones;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Catalogos
{
    [Collection(IntegrationTestsCollection.Name)]
    public class CatalogoAsuntosTest : IClassFixture<TestStartup>, IAsyncLifetime
    {
        private readonly TestsInitializer _testsInitializer;
        private readonly CatalogoAsuntoFunction? _sut;
        private readonly ILogger<CatalogoAsuntosTest> _logger;

        public CatalogoAsuntosTest(TestsInitializer testsInitializer)
        {
            _testsInitializer = testsInitializer;
            _sut = _testsInitializer.ServiceProvider.GetService<CatalogoAsuntoFunction>();
            _logger = _testsInitializer.ServiceProvider.GetService<ILogger<CatalogoAsuntosTest>>();
        }

        [Fact]
        public async void Post_ShouldCreateNote_WhenCalledWithValidNoteDetails()
        {
            // Arrange
            var req = TestCommon.CreateMockRequest("{}");

            var response = await _sut!.Run(req.Object, _logger);
            var okResult = response as OkObjectResult;

            // Assert
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            okResult.Value.Should().NotBeNull();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        public async Task InitializeAsync()
        {
        }
    }
}
