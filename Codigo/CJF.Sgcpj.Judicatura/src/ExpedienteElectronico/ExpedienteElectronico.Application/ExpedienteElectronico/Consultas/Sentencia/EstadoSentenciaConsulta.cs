using MediatR;

namespace CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.Sentencia;
public class EstadoSentenciaConsulta : IRequest<EstadoSentenciaDto>
{
    public long AsuntoNeunId { get; set; }
}
