using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.GenerarOficio;
public class GenerarOficioRequestDto : IRequest<ResponseDatosGenerarFolioM>
{
    public long AsuntoNeunId { get; set; }
    public long AsuntoDocumentoId { get; set; }
    public int CatTipoAsunto { get; set; }
    public string AsuntoAlias { get; set; }
    public int Mesa { get; set; }
    public List<PersonasDto> PartesLista { get; set; }
    public int AsuntoDocumentoIdOficio { get; set; }
}
