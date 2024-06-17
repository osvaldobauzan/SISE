using CJF.Sgcpj.Judicatura.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Oficialia.Domain.Entities;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtienePromocionesConFiltro;
public class DetalleDto : IMapFrom<Detalle>
{
    public string Folio { get; set; }
    public string Tipo { get; set; }

    public DateTime FechaDeRegistro { get; set; }
    public string Usuario { get; set; }

    public string NumeroDeArchivos { get; set; }
    public string Firmado { get; set; }

    public string NoRegistroOCC { get; set; }

    public DateTime RegistroOCC { get; set; }
}

public class ExpedienteDto : IMapFrom<Expediente>
{
    public int AsuntoNeunId { get; set; }
    public string AsuntoAlias { get; set; }
    public int CatTipoOrganismoId { get; set; }
    public int CatOrganismoId { get; set; }
    public string CatTipoAsunto { get; set; }
    public string CatTipoAsuntoId { get; set; }
    public string TipoProcedimiento { get; set; }
    public string NombreCorto { get; set; }
}

public class PromocionDto : IMapFrom<Promocion>
{
    public ExpedienteDto Expediente { get; set; }
    public int NumeroOrden { get; set; }
    public int YearPromocion { get; set; }
    public int OrigenPromocion { get; set; }
    public int Origen { get; set; }
    public string NombreOrigen { get; set; }
    public string OrigenPromocionDescripcion { get; set; }
    public int NumeroRegistro { get; set; }
    public DateTime FechaPresentacion { get; set; }
    public DateTime FechaPresentacionFin { get; set; }
    public string Mesa { get; set; }
    public int ClasePromocion { get; set; }
    public string ClasePromocionDescripcion { get; set; }
    public int Copias { get; set; }
    public int Anexos { get; set; }
    public string Color { get; set; }
    public int CuadernoId { get; set; }
    public int SecretarioId { get; set; }
    public string CuadernoNombreCorto { get; set; }
    public string CuadernoNombre { get; set; }
    public string SecretarioDescripcion { get; set; }
    public string SecretarioUserName { get; set; }
    public string SecretarioNombres { get; set; }
    public string SecretarioPaterno { get; set; }
    public string SecretarioMaterno { get; set; }
    public string TipoContenidoDescripcion { get; set; }
    public int TipoContenidoId { get; set; }
    public string ParteDescripcion { get; set; }
    public int TipoPromovente { get; set; }
    public int ClasePromoventeId { get; set; }
    public string ClasePromoventeDescripcion { get; set; }
    public bool EsDemandaElectronica { get { return EsDemanda && EsPromocionE; } } // EsDemanda = 1 y EsPromocionE = 1
    public bool EsDemanda { get; set; } // EsDemanda = 1 y EsPromocionE = 0
    public bool EsPromocionE { get; set; }  // EsDemanda = 0 y EsPromocionE = 1
    public bool CambioDemandaPromocion { get; set; }
    public bool ConAcuerdo { get; set; }
    public int Estado { get; set; }
    public int EstadoAcuerdo { get; set; }
    public Int16 Fojas { get; set; }
    public string? kIdElectronica { get; set; }
    public string UsuarioCaptura { get; set; }
    public DateTime? FechaCaptura { get; set; }
    public DetalleDto Detalle { get; set; }
    public int? CatAutorizacionDocumentosId { get; set; }
    public bool ConArchivo { get; set; }
    public string? NombreOficial { get; set; }
    public int? Firmado { get; set; }



}
