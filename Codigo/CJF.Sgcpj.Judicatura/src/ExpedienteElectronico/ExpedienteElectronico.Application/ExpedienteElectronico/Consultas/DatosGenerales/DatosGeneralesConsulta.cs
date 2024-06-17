using MediatR;

namespace ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.DatosGenerales;
public class DatosGeneralesConsulta : IRequest<DatosGeneralesDto>
{
    public long AsuntoNeunId { get; set; }
}
