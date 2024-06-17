using NUnit.Framework;
using FluentAssertions;

using CJF.Sgcpj.Judicatura.Infrastructure.Services;
using CJF.Sgcpj.Judicatura.Application.Common.Interfaces;

namespace CJF.Sgcpj.Judicatura.Application.UnitTests.Common.Services;

public class EmailMessageUnitTest
{
    [Test]
    public async Task ShouldSendEmail()
    {
        IEmailMessage emailMessage = new EmailMessageService(SetUp.GenerateConfiguration(), null);

        var to = "pablo.ramirez@mobiik.com";
        var resultado = await emailMessage.SendEmail(to, "Subject Test", "test email");

        resultado.IsSuccess.Should().BeTrue();
        resultado.ErrorMessage.Should().BeNull();
    }
}
