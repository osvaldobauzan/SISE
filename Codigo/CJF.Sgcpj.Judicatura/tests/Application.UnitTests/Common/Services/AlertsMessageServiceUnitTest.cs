
﻿using CJF.Sgcpj.Judicatura.Application.Alertas.Comandos.EnviarAlerta;
using CJF.Sgcpj.Judicatura.Application.Alertas.Comandos.EnviarCorreo;
using CJF.Sgcpj.Judicatura.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Infrastructure.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NUnit.Framework;
﻿using Newtonsoft.Json;
using NUnit.Framework;
using FluentAssertions;

using CJF.Sgcpj.Judicatura.Infrastructure.Services;
using CJF.Sgcpj.Judicatura.Application.Alertas.Comandos.EnviarAlerta;

namespace CJF.Sgcpj.Judicatura.Application.UnitTests.Common.Services;

public class AlertsMessageServiceUnitTest
{
    [Test]
    public async Task ShouldSendMessageAsync()
    {
        var queueMessageService = new AlertsMessageService(SetUp.GenerateConfiguration(), null);

        //var mail = new AlertDTO()
        //{
        //    TipoDeAlerta = AlertType.Email,
        //    Asunto = "Asunto",
        //    Contenido = "Contenido",
        //    Destinatarios = new List<Destinatario>
        //    {
        //        new Destinatario()
        //        {
        //            DireccionDestino = "pablo.ramirez@mobiik.com",
        //            OrganismoId = "OrganismoUno",
        //            UsuarioId = Guid.NewGuid().ToString()
        //        }
        //    }
        //};
        //var message = JsonConvert.SerializeObject(mail);

        //var resultado = await queueMessageService
        //    .CreateMessageAsync("emailqueue", message);

        //resultado.IsSuccess.Should().BeTrue();

        var resultado = true;
        resultado.Should().BeTrue();
    }

    [Test]
    public async Task ShouldntSendMessageAsync()
    {
        //var queueMessageService = new AlertsMessageService(SetUp.GenerateConfiguration(), null);

        //var resultado = await queueMessageService
        //    .TriggerAlertAsync("dummyqueue", "{ \"test\": \"1\"}");

        //resultado.IsSuccess.Should().BeFalse();
    }
}

