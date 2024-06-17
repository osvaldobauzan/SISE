using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Common.Services;

[Collection(IntegrationTestsCollection.Name)]
public class RutasChunkServiceTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;

    public RutasChunkServiceTests(TestsInitializer testsInitializer)
    {
        _testsInitializer = testsInitializer;
    }

    public async Task DisposeAsync()
    {
    }

    public async Task InitializeAsync()
    {
    }

    [Fact]
    public async Task RutasChunkShouldReturnNull()
    {
        var service = _testsInitializer.ServiceProvider.GetService<IRutasChunkService>();
        var result = await service.RutasChunkPorModuloAsync
            ($"\\\\10.100.126.204\\desa_fs\\AcuerdosP5\\180\\50019.docx", RutasChunkModulos.Tramite);

        result.Should().BeNull();
    }
}
