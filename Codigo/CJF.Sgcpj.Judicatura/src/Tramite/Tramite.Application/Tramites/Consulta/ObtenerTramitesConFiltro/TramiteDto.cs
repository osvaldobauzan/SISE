using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Tramite.Domain.Entities;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerTramitesConFiltro;

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
public class TramiteDto : IMapFrom<Domain.Entities.Tramite>
{
    public ExpedienteDto Expediente { get; set; }
    public int NumeroRegistro { get; set; }
    public string TipoPromocionDescripcion { get; set; }
    public DateTime FechaRecibido { get; set; }
    public int NumeroOrden { get; set; }
    public string NombreTipoCuaderno { get; set; }
    public string NombreParte { get; set; }
    public string TipoContenidoDescripcion { get; set; }
    public string Contenido { get; set; }
    public int Copias { get; set; }
    public int Anexos { get; set; }
    public string Estado { get; set; }
    public string Mesa { get; set; }
    public long SecretarioId { get; set; }
    public string SecretarioDescripcion { get; set; }
    public DateTime? FechaAuto { get; set; }
    public string Plantilla { get; set; }
    public int AsuntoId { get; set; }
    public int AsuntoDocumentoId { get; set; }
    public string NombreArchivo { get; set; }
    public string NombreCapDJ { get; set; }
    public int EstadoAutorizacion { get; set; }
    public long NumeroAlias { get; set; }
    public string ArchivoPromocion { get; set; }
    public string NombreOrigen { get; set; }
    public int Origen { get; set; }
    public string UserNameOficial { get; set; }
    public string EmpleadoCancela { get; set; }
    public string EmpleadoAutoriza { get; set; }
    public string EmpleadoPreAutoriza { get; set; }
    public string EmpleadoElimina { get; set; }
    public DateTime? FechaPreAutoriza { get; set; }
    public DateTime? FechaAutoriza { get; set; }
    public DateTime? FechaCancela { get; set; }
    public string FechaElimina { get; set; }
    public string UserNameCapDJ { get; set; }
    public string UserNameSecretario { get; set; }
    public string FechaRecibido_F { get; set; }
    public string FechaAuto_F { get; set; }
    public string NombreDocumento { get; set; }
    public int YearPromocion { get; set; }
    public int TipoCuadernoId { get; set; }
    public string NombreCorto { get; set; }
    public int RutaArchivoNAS { get; set; }
    public string Promovente { get; set; }
    public int SintesisOrden { get; set; }
    public Guid GuidDocumento { get; set; }
    public bool? EsPromocionE { get; set; }
    public int? OficiosFirmados { get; set; }
    public int? CanceloCuenta { get; set; }
    public bool? PromocionCompleta { get; set; }
}