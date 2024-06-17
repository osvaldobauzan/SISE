using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Common.Services;

[Collection("Services")]
public class DateTimeServiceTests
{
    [Fact]
    public async void InsertarQRTestShouldReturnOK()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<IDateTime, DateTimeService>();
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var service = serviceProvider.GetService<IDateTime>();
        var result = service.Now;

        result.Year.Should().Be(DateTime.Now.Year);
    }
}
