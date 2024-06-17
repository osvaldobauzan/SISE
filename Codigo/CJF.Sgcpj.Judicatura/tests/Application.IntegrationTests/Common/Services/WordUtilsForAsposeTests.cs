using System.Text;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Common.Services;

[Collection(IntegrationTestsCollection.Name)]
public class WordUtilsForAsposeTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;

    public WordUtilsForAsposeTests(TestsInitializer testsInitializer)
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
    public async void InsertQRCodeInWordDocumentShouldReturnOK()
    {
        var html = $"<div> Test </div>";
        var service = _testsInitializer.ServiceProvider.GetService<IWordsUtil>();

        var bytes = Encoding.UTF8.GetBytes(html);
        var result = service.InsertQRCodeInWordDocument(bytes, "qr1", "qrCodeText", 100);

        result.Should().NotBeNull();
        result.Length.Should().BeGreaterThan(0);
    }

    [Fact]
    public async void RemoveImageShouldReturnOK()
    {
        var html = $"<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<rootElement><note>\r\n<to>Tove</to>\r\n<from>Jani</from>\r\n<heading>Reminder</heading>\r\n<body>Don't forget me this weekend!</body>\r\n</note></rootElement>";
        var service = _testsInitializer.ServiceProvider.GetService<IWordsUtil>();

        var bytes = Encoding.UTF8.GetBytes(html);
        var result = service.RemoveImage(bytes, $"imageToRemove");

        result.Should().NotBeNull();
        result.Length.Should().BeGreaterThan(0);
    }

    [Fact]
    public async void ReplaceTextInDocxShouldReturnOK()
    {
        var html = $"<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<rootElement><note>\r\n<to>Tove</to>\r\n<from>Jani</from>\r\n<heading>Reminder</heading>\r\n<body>Don't forget me this weekend!</body>\r\n</note></rootElement>";
        var service = _testsInitializer.ServiceProvider.GetService<IWordsUtil>();

        var bytes = Encoding.UTF8.GetBytes(html);
        var result = service.ReplaceTextInDocx(bytes, $"searchText", $"replaceText");

        result.Should().NotBeNull();
        result.Length.Should().BeGreaterThan(0);
    }
}
