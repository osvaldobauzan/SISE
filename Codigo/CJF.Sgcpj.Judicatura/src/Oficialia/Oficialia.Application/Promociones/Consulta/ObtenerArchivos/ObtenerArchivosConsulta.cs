using CJF.Sgcpj.Judicatura.Application.Common.Models;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerPromocion;
public record ObtenerArchivosConsulta : IRequest<ArchivosPromocion>
{
    public long AsuntoNeunId { get; set; }
    public int YearPromocion { get; set; }
    public int NumeroOrden { get; set; }
    public int CatIdOrganismo { get; set; }
    public int Origen { get; set; }
    public int TipoModulo { get; set; }
    public int NumeroRegistro { get; set; }
    public int AsuntoDocumentoId { get; set; }
    public long? kIdElectronica { get; set; }

}
