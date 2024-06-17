namespace CJF.Sgcpj.Judicatura.Application.UnitTests.Common.Services;

using NUnit.Framework;
using FluentAssertions;
using Microsoft.Extensions.Configuration;

using CJF.Sgcpj.Judicatura.Infrastructure.Services;
using CJF.Sgcpj.Judicatura.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Application.Common.Interfaces;

public class WordUtilsForOpenXMLUnitTest
{
    [Test]
    public void should_saveReplaceTextInDocxByOpenXML()
    {
        //Arrage
        IConfiguration _configuration = SetUp.GenerateConfiguration();
        IWordsUtil _wordUtilsForOpenXML = new WordUtilsForOpenXML();
        string docxInBase64 = _configuration["base64docxString"];
        string searchText = "Texto a reemplazar";
        string replaceText = $"Texto reemplazado {DateTime.Now.ToString()}";
        //Act
        var resultado = _wordUtilsForOpenXML.ReplaceTextInDocx(Convert.FromBase64String(docxInBase64), searchText, replaceText);

        string utfString = Convert.ToBase64String(resultado);
        //Assert
        utfString.Should().NotBeNullOrEmpty();
    }

    [Test]
    public void should_saveInsertQRCodeInDocxByOpenXML()
    {
        //Arrage
        IGeneradorQR _generadorQR = new GeneradorQRService();
        IConfiguration _configuration = SetUp.GenerateConfiguration();
        IWordsUtil _wordUtilsForOpenXML = new WordUtilsForOpenXML(_generadorQR);
        string docxInBase64 = _configuration["base64docxString"];
        string searchText = "https://www.google.com.mx";

        //Act
        var resultado = _wordUtilsForOpenXML.InsertQRCodeInWordDocument(Convert.FromBase64String(docxInBase64), searchText, 8);

        string utfString = Convert.ToBase64String(resultado);
        //Assert
        utfString.Should().NotBeNullOrEmpty();
    }


}
