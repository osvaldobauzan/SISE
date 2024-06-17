﻿using MediatR;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ValidarExpediente;

public class ValidarExpedienteConsulta : IRequest<ValidacionExpedienteDto>
{
    public int CatOrganismoId { get; set; }

    public int CatCuadernoId { get; set; }

    public string? NumeroExpediente { get; set; }

    public int? CatTipoAsuntoId { get; set; }

    public long? AsuntoNeunId { get; set; }
}
