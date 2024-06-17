using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Common.Services;

[Collection(IntegrationTestsCollection.Name)]
public class UserConnectionsHandlerTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;

    public UserConnectionsHandlerTests(TestsInitializer testsInitializer)
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
    public async Task UserConnectionsHandlerLifecycleShouldReturnOk()
    {
        var service = _testsInitializer.ServiceProvider.GetService<IUserConnectionsHandler>();

        var resultQuery = await service.QueryConnectionAsync("6712");
        resultQuery.Results.Should().NotBeNullOrEmpty();

        var resultDelete = await service.RemoveConnectionAsync("test_connection", "6712");
        resultDelete.IsSuccess.Should().Be(true);
    }
}
