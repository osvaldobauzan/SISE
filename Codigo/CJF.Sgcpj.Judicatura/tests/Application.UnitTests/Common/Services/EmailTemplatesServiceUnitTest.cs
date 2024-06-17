using NUnit.Framework;
using FluentAssertions;

using CJF.Sgcpj.Judicatura.Infrastructure.Services;

namespace CJF.Sgcpj.Judicatura.Application.UnitTests.Common.Services;

public class EmailTemplatesServiceUnitTest
{
    [Test]
    public async Task ShouldDownloadBlob()
    {
        var emailTemplatesMessage = new EmailTemplatesService(null, SetUp.GenerateConfiguration());

        var resultado = await emailTemplatesMessage.GetTemplateAsync("01.txt");

        resultado.IsSuccess.Should().BeTrue();
        resultado.Template.Should().NotBeNull();
        resultado.ErrorMessage.Should().BeNull();
    }

    [Test]
    public async Task ShouldReturnFalseIfBlobDoesntExists()
    {
        var emailTemplatesMessage = new EmailTemplatesService(null, SetUp.GenerateConfiguration());

        var resultado = await emailTemplatesMessage.GetTemplateAsync("dummy.txt");

        resultado.IsSuccess.Should().BeFalse();
        resultado.Template.Should().BeNull();
        resultado.ErrorMessage.Should().NotBeNull();
    }
}
