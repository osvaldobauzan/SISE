using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Seguimientos.Consulta;
public record class ObtenerSeguimientoConsultaXAliasAsunto : IRequest<IEnumerable<Common.Models.Seguimiento>>
{
    public Common.Models.Seguimiento seguimiento { get; set; }
}

public class ObtieneSeguimientoXAliasAsuntoHandler : IRequestHandler<ObtenerSeguimientoConsultaXAliasAsunto, IEnumerable<Common.Models.Seguimiento>>
{
    private readonly ISeguimientoRepository _seguimientoRepository;

    public ObtieneSeguimientoXAliasAsuntoHandler(ISeguimientoRepository seguimientoRepository)
    {
        _seguimientoRepository = seguimientoRepository;
    }

    public async Task<IEnumerable<Common.Models.Seguimiento>> Handle(ObtenerSeguimientoConsultaXAliasAsunto seguimiento, CancellationToken cancellationToken)
    {
        return await _seguimientoRepository.getAllExpediente(seguimiento.seguimiento); 
    }
}

