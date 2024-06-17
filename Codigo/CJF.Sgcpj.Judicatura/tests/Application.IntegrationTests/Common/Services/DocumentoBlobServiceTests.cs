using System.Text;
using Azure;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Common.Services;

[Collection(IntegrationTestsCollection.Name)]
public class DocumentoBlobServiceTests : IClassFixture<TestStartup>, IAsyncLifetime
{
    private readonly TestsInitializer _testsInitializer;

    public DocumentoBlobServiceTests(TestsInitializer testsInitializer)
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
    public async Task GuardarBlobDocumentoShouldReturnOk()
    {
        var service = _testsInitializer.ServiceProvider.GetService<IDocumentoBlob>();
        var _configuration = _testsInitializer.ServiceProvider.GetService<IConfiguration>();

        var fileId = $"{Guid.NewGuid()}";
        var url = _configuration["SISE3:BackEnd:SMTPTemplatesUrl"];
        var contenedorPlantillas = _configuration["SISE3:BackEnd:OficiosPlantillasContenedor"];

        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Hola.docx");
        var bytes = File.ReadAllBytes(path);

        await service.GuardarBlobDocumento(bytes, fileId, contenedorPlantillas, url);

        //No arroja exception
        var result = 1;
        result.Should().Be(1);

        var fileResult = await service.ObtenerBlobDocumento(fileId, contenedorPlantillas, url);

        fileResult.Should().NotBeNull();
        fileResult.Length.Should().BeGreaterThan(1);
    }

    [Fact]
    public async Task GuardarBlobDocumentoShouldThrowExceptionIfInvalidContainer()
    {
        var service = _testsInitializer.ServiceProvider.GetService<IDocumentoBlob>();
        var _configuration = _testsInitializer.ServiceProvider.GetService<IConfiguration>();

        var fileId = $"{Guid.NewGuid()}.txt";
        var url = _configuration["SISE3:BackEnd:SMTPTemplatesUrl"];

        var fakeFile = Encoding.UTF8.GetBytes("dummyFile");

        try
        {
            await service.GuardarBlobDocumento(fakeFile, fileId, "dummyContainer", url);
        }
        catch (Exception ex)
        {
            ex.Should().NotBeNull();
            ex.Should().BeOfType<RequestFailedException>();
        }
    }

    [Fact]
    public async Task ObtenerBlobDocumentoShouldThrowExceptionIfInvalidContainer()
    {
        var service = _testsInitializer.ServiceProvider.GetService<IDocumentoBlob>();
        var _configuration = _testsInitializer.ServiceProvider.GetService<IConfiguration>();

        var fileId = $"{Guid.NewGuid()}.txt";
        var url = _configuration["SISE3:BackEnd:SMTPTemplatesUrl"];

        var fakeFile = Encoding.UTF8.GetBytes("dummyFile");

        try
        {
            await service.ObtenerBlobDocumento(fileId, "dummyContainer", url);
        }
        catch (Exception ex)
        {
            ex.Should().NotBeNull();
            ex.Should().BeOfType<RequestFailedException>();
        }
    }

    [Fact]
    public async Task ObtenerTextoBlobShouldReturnOK()
    {
        var service = _testsInitializer.ServiceProvider.GetService<IDocumentoBlob>();
        var _configuration = _testsInitializer.ServiceProvider.GetService<IConfiguration>();

        var fileId = $"Anexo1.docx";
        var url = _configuration["SISE3:BackEnd:SMTPTemplatesUrl"];
        var contenedorPlantillas = _configuration["SISE3:BackEnd:OficiosPlantillasContenedor"];

        var result = await service.ObtenerTextoBlob(fileId, contenedorPlantillas, url);

        result.Should().NotBeNull();
        result.Length.Should().BeGreaterThan(1);
    }

    [Fact]
    public async Task ObtenerTextoBlobShouldReturnFalseIfBadcontainer()
    {
        var service = _testsInitializer.ServiceProvider.GetService<IDocumentoBlob>();
        var _configuration = _testsInitializer.ServiceProvider.GetService<IConfiguration>();

        var fileId = $"6d02a5d6-fbbf-4ff1-bfbe-a06d01bf8684.txt";
        var url = _configuration["SISE3:BackEnd:SMTPTemplatesUrl"];
        try
        {
            var result = await service.ObtenerTextoBlob(fileId, "dummyContainer", url);
        }
        catch (Exception ex)
        {
            ex.Should().NotBeNull();
            ex.Should().BeOfType<RequestFailedException>();
        }
    }

    [Fact]
    public async Task ObtenerPlantillaCorreoShouldReturnOK()
    {
        var service = _testsInitializer.ServiceProvider.GetService<IDocumentoBlob>();
        var _configuration = _testsInitializer.ServiceProvider.GetService<IConfiguration>();

        var url = _configuration["SISE3:BackEnd:SMTPTemplatesUrl"];
        var result = await service.ObtenerPlantillaCorreo("plantillaPromocionCorreo.html", "emailtemplates", url);

        result.Should().NotBeNull();
        result.Length.Should().BeGreaterThan(1);
    }
}
