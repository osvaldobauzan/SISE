using CJF.Sgcpj.Judicatura.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerPromocionDetalle;
public class AnexoListaDto : IMapFrom<AnexosLista>
{
    public string? Tipo { get; set; }
    public long? AsuntoNeunId { get; set; }
    public string? Expediente { get; set; }
    public string? CatTipoAsunto { get; set; }
    public string? TipoProcedimiento { get; set; }
    public long? NumeroRegistro { get; set; }
    public string? OrigenPromocion { get; set; }
    public DateTime? FechaPresentacion { get; set; }
    public string? Promovente { get; set; }
    public int? OrigenPromocionId { get; set; }
    public long? Folio { get; set; }
    public long? OCC { get; set; }
    public long? BoletaOCC { get; set; }
    public int? TotalArchivos { get; set; }
    public string? Archivos { get; set; }
    public string? PromoventeNombre { get; set; }
    public string? PromoventeApellidoPaterno { get; set; }
    public string? PromoventeApellidoMaterno { get; set; }
}