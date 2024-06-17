﻿using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Actuario.GenerarAcuseOficio.Actuario;
public class GenerarAcuseOficioRequestDto : IRequest<GenerarAcuseOficioResponseDto>
{
    public long AsuntoNeunId { get; set; }
    public int AsuntoDocumentoId { get; set; }
    public long ActuarioId { get; set; }
    public int CatOrganismoId { get; set; }
}
