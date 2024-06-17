using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Comandos.EstadoAcuerdoComando;
public class EstadoAcuerdoDto : IMapFrom<EstadoAcuerdo>
{
    public long AsuntoNeunId { get; set; }
    public int AsuntoDocumentoId { get; set; }
    public int TipoUpdate { get; set; }
    public long Valor { get; set; }
    public string NombreDocumento { get; set; }
}
