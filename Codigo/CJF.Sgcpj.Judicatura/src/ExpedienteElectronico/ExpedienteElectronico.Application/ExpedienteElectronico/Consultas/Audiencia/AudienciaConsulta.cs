using MediatR;

namespace CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.Audiencia;
public class AudienciaConsulta : IRequest<AudienciaDto>
{
    public long AsuntoNeunId { get; set; }
    public int CuadernoId { get; set; }
}
