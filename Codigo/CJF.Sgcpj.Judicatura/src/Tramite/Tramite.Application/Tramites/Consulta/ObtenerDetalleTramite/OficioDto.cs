using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerDetalleTramite;
public class OficioDto : IMapFrom<Oficio>
{
    public int? AsuntoId { get; set; }
    public long? AsuntoNeunId { get; set; }
    public int? AsuntoDocumentoId { get; set; }
    public int? AnexoId { get; set; }
    public short? AnexoTipoId { get; set; }
    public int? AnexoParteId { get; set; }
    public string? AnexoParteDescripcion { get; set; }
    public string? Texto { get; set; }
    public string? NombreDocumento { get; set; }
    public int? RutaArchivoNAS { get; set; }
    public string? RutaAnexo { get; set; }
    public string? NombreArchivo { get; set; }

}
