using NUnit.Framework;
using FluentAssertions;

using Microsoft.Extensions.Configuration;

using CJF.Sgcpj.Judicatura.Infrastructure.Services;
using CJF.Sgcpj.Judicatura.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Application.Common.Interfaces;
using Moq;

namespace CJF.Sgcpj.Judicatura.Application.UnitTests.Common.Services;
public class WordUtilsTest
{
    [Test]
    public void ShouldBeInsertTextInDocx()
    {
        //Arrage
        IConfiguration configuration = SetUp.GenerateConfiguration();
        IGeneradorQR generadorQR = new GeneradorQRService();
        IWordsUtil officeValidator = new WordUtilsForAspose(generadorQR);
        string docxInBase64 = configuration["base64docxString"];
        string textToReplace = "demo reemplazado";
        string textFindToReplace = "Texto a reemplazar";
        //Act
        var resultado = officeValidator.ReplaceTextInDocx(Convert.FromBase64String(docxInBase64), textFindToReplace, textToReplace);

        string utfString = Convert.ToBase64String(resultado);
        //Assert
        utfString.Should().NotBeNullOrEmpty();
    }

    [Test]
    public void ShouldBeInsertTextInDocx_WhenTextSearchIsNull()
    {
        //Arrage
        IConfiguration configuration = SetUp.GenerateConfiguration();
        IGeneradorQR generadorQR = new GeneradorQRService();
        IWordsUtil officeValidator = new WordUtilsForAspose(generadorQR);
        string docxInBase64 = configuration["base64docxString"];
        string textToReplace = "demo reemplazado";
        string textFindToReplace = "Texto a reemplazar";

        //Act and assert
        var resultado = Assert.Throws<ArgumentNullException>(() =>
            officeValidator.ReplaceTextInDocx(Convert.FromBase64String(docxInBase64), null, textToReplace));

        //Verify 
        Assert.AreEqual("Value cannot be null.", resultado.Message);
    }

    [Test]
    public void ShouldBeInsertTextInDocx_WhenBytesArrayIsNull()
    {
        //Arrage
         IGeneradorQR generadorQR = new GeneradorQRService();
        IConfiguration configuration = SetUp.GenerateConfiguration();
        IWordsUtil officeValidator = new WordUtilsForAspose(generadorQR);
        string textToReplace = "demo reemplazado";
        string textFindToReplace = "Texto a reemplazar";

        //Act and assert
        var resultado = Assert.Throws<ArgumentNullException>(() =>
            officeValidator.ReplaceTextInDocx(null, textFindToReplace, textToReplace));

        //Verify 
        Assert.AreEqual("Value cannot be null.", resultado.Message);
    }

    [Test]
    public void ShouldBeInsertQRInDocx()
    {
        //Arrage
        IGeneradorQR generadorQR = new GeneradorQRService();
        IConfiguration configuration = SetUp.GenerateConfiguration();
        IWordsUtil officeValidator = new WordUtilsForAspose(generadorQR);
        string docxInBase64 = configuration["base64docxString"];

        //Act
        var resultado = officeValidator.InsertQRCodeInWordDocument(Convert.FromBase64String(docxInBase64), "Texto a reemplazar", 45);

        string utfString = Convert.ToBase64String(resultado);
        //Assert
        utfString.Should().NotBeNullOrEmpty();
    }
}
