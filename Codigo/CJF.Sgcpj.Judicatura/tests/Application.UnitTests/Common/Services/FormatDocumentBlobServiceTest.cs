namespace CJF.Sgcpj.Judicatura.Application.UnitTests.Common.Services;

using NUnit.Framework;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

using CJF.Sgcpj.Judicatura.Application.Utils;
using CJF.Sgcpj.Judicatura.Infrastructure.Services;
using CJF.Sgcpj.Judicatura.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Application.Common.Interfaces;

public class FormatDocumentBlobServiceTest
{
    [Test]
    public async Task should_getDocxInBlob_Ok()
    {
        //Arrage
        IConfiguration _configuration = SetUp.GenerateConfiguration();
        IDocumentoBlob _docBlob = new DocumentoBlobService();


        var contenedorPlantillas = _configuration["SISE3:BackEnd:OficiosPlantillasContenedor"];
        var uri = _configuration["SISE3:BackEnd:SMTPTemplatesUrl"];

        string strFileInBlob = "POO Definiciones.docx";

        //Act
        var resultado = await _docBlob.ObtenerBlobDocumento(strFileInBlob,contenedorPlantillas,uri);

        //Assert
        string utfString = Convert.ToBase64String(resultado);
        utfString.Should().NotBeNullOrEmpty();
    }

    [Test]
    public async Task should_getDocxInBlob_Exception()
    {
        // Arrage
        IConfiguration _configuration = SetUp.GenerateConfiguration();
        var uri = _configuration["SISE3:BackEnd:SMTPTemplatesUrl"];
        var contenedorPlantillas = _configuration["SISE3:BackEnd:OficiosPlantillasContenedor"];
        IDocumentoBlob _docBlob = new DocumentoBlobService();
        string strFileInBlob = "POO Definiciones1.docx";

        // Act and Assert
        Assert.That(() => _docBlob.ObtenerBlobDocumento(strFileInBlob, contenedorPlantillas,uri), Throws.Exception);
    }

    [Test]
    public async Task should_saveBytesInDocxInBlob()
    {
        //Arrage
        IConfiguration _configuration = SetUp.GenerateConfiguration();
        IDocumentoBlob _docBlob = new DocumentoBlobService();
        string docxInBase64 = _configuration["base64docxString"];
        string strFileInBlob = "POO Definiciones1.docx";
        var uri = _configuration["SISE3:BackEnd:SMTPTemplatesUrl"];
        var contenedorOficiosGenerados = _configuration["SISE3:BackEnd:OficiosContenedor"];
        //Act
        await _docBlob.GuardarBlobDocumento(Convert.FromBase64String(docxInBase64), strFileInBlob,contenedorOficiosGenerados,uri);

        //Assert
        Assert.IsTrue(true);
    }

    [Test]
    public async Task should_getReplaceInBlobDocByAspose_Ok()
    {
        //Arrage
        IConfiguration _configuration = SetUp.GenerateConfiguration();
        IDocumentoBlob _docBlob = new DocumentoBlobService();
        IGeneradorQR _generadorQR = new GeneradorQRService();
        IWordsUtil _wordUtils = new WordUtilsForAspose(_generadorQR);
        IFormatDocumentBlobService _docBlobFormat = new FormatDocumentBlobService(_wordUtils, _configuration, _docBlob);
        string strFileInBlob = "POO Definiciones.docx";

        //Act
        var resultado = await _docBlobFormat.ReplaceTextInBlobDocumentForAspose(strFileInBlob, "Python", "C#");
        Functions.BytesToLocalFile(resultado, $"{Functions.GenerateStringRandom(25)}.docx");

        //Assert
        Convert.ToBase64String(resultado).Should().NotBeNullOrEmpty();
    }

    [Test]
    public async Task should_getReplaceInBlobDocByOpenXML_Ok()
    {
        //Arrage
        IConfiguration _configuration = SetUp.GenerateConfiguration();
        IDocumentoBlob _docBlob = new DocumentoBlobService();
        IWordsUtil _wordUtils = new WordUtilsForOpenXML();
        IFormatDocumentBlobService _docBlobFormat = new FormatDocumentBlobService(_wordUtils, _configuration, _docBlob);
        string strFileInBlob = "POO Definiciones.docx";

        //Act
        var resultado = await _docBlobFormat.ReplaceTextInBlobDocumentForOpenXML(strFileInBlob, "Python", "C#");
        Functions.BytesToLocalFile(resultado, $"{Functions.GenerateStringRandom(25)}.docx");

        //Assert
        Convert.ToBase64String(resultado).Should().NotBeNullOrEmpty();
    }
    //Todo que tenga el que obtenga el archivo sea el blobservice
    //[Test]
    //public async Task should_getInsertInQRInBlobDocByAspose_Ok()
    //{
    //    //Arrage
    //    IGeneradorQR _generadorQR = new GeneradorQRService();
    //    IConfiguration _configuration = SetUp.GenerateConfiguration();
    //    IDocumentoBlob _docBlob = new DocumentoBlobService();
    //    IWordsUtil _wordUtils = new WordUtilsForAspose(_configuration, _generadorQR);
    //    string strFileInBlob = "POO Definiciones.docx";

    //    //Act
    //    var resultado = await _wordUtils.InsertQRCodeInWordDocument(strFileInBlob, "www.google.com.mx",40);
    //    Functions.BytesToLocalFile(resultado, $"{Functions.GenerateStringRandom(25)}.docx");

    //    //Assert
    //    Convert.ToBase64String(resultado).Should().NotBeNullOrEmpty();
    //}

    [Test]
    public async Task should_getInsertInQRInBlobDocByOpenXML_Ok()
    {
        //Arrage
        IGeneradorQR _generadorQR = new GeneradorQRService();
        IConfiguration _configuration = SetUp.GenerateConfiguration();
        IDocumentoBlob _docBlob = new DocumentoBlobService();
        IWordsUtil _wordUtils = new WordUtilsForOpenXML(_generadorQR);
        IFormatDocumentBlobService _docBlobFormat = new FormatDocumentBlobService(_wordUtils, _generadorQR, _configuration, _docBlob);
        string strFileInBlob = "POO Definiciones.docx";

        //Act
        var resultado = await _docBlobFormat.InsertQRDocumentForOpenXML(strFileInBlob, "www.google.com.mx", 12);
        Functions.BytesToLocalFile(resultado, $"{Functions.GenerateStringRandom(25)}.docx");

        //Assert
        Convert.ToBase64String(resultado).Should().NotBeNullOrEmpty();
    }
}
