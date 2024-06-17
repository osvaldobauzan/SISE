using MediatR;

namespace ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.InformacionParte;
public class InformacionParteConsulta : IRequest<List<InformacionParteDto>>
{
    public long AsuntoNeunId { get; set; }
    public long PersonaId { get; set; }
}
