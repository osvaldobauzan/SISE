namespace CJF.Sgcpj.Judicatura.Application.UnitTests.Common.Services;

using NUnit.Framework;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

using CJF.Sgcpj.Judicatura.Infrastructure.Services;
using CJF.Sgcpj.Judicatura.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Application.Utils;

public class DocumentoBlobServiceUnitTest
{
    [Test]
    public async Task ShouldObtenerBlobDocumento()
    {
        var configuration = SetUp.GenerateConfiguration();
        IDocumentoBlob obtenerDocumento = new DocumentoBlobService();
        string fileId = "plantilla_ejemplo_1.docx";
        var contenedorPlantillas = configuration["SISE3:BackEnd:OficiosPlantillasContenedor"];
          var uri = configuration["SISE3:BackEnd:SMTPTemplatesUrl"];
        var resultado = await obtenerDocumento.ObtenerBlobDocumento(fileId, contenedorPlantillas, uri);

        resultado.Should().NotBeNull();

    }
    [Test]
    public async Task ShouldGuardarBlobDocumento()
    {
        IConfiguration configuration = SetUp.GenerateConfiguration();

        var uri = configuration["SISE3:BackEnd:SMTPTemplatesUrl"];
        var contenedorOficiosGenerados = configuration["SISE3:BackEnd:OficiosContenedor"];


        IDocumentoBlob obtenerDocumento = new DocumentoBlobService();
        string fileId = $"{Functions.GenerateStringRandom(15)}.docx";
        byte[] archivo = Convert.FromBase64String(configuration["base64docxString"]);

        await obtenerDocumento.GuardarBlobDocumento(archivo, fileId, contenedorOficiosGenerados, uri);

    }
}
