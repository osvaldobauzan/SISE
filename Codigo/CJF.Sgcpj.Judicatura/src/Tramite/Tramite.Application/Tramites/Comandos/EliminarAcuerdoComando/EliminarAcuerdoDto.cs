using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Comandos.EliminarAcuerdoComando;
public class EliminarAcuerdoDto : IMapFrom<EliminarAcuerdo>
{
    public long AsuntoNeunId { get; set; }
    public int AsuntoDocumentoId { get; set; }
    public int catIdOrganismo { get; set; }
}
