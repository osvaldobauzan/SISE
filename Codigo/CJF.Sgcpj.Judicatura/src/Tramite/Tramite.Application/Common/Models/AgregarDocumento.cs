
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerArchivos;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;

public class AgregarDocumento
{
    public long AsuntoNeunId { get; set; }
    public int AsuntoId { get; set; }
    public int YearPromocion { get; set; }
    public int NumeroOrden { get; set; }
    public int RegistroEmpleadoId { get; set; }
    public int Clase { get; set; }
    public int Descripcion { get; set; }
    public int Caracter { get; set; }
    public int Origen { get; set; }
    public int? NumeroConsecutivo { get; set; }
    public int CatOrganismoId { get; set; }
    public int NumeroRegistro { get; set; }
    public short Fojas { get; set; }
    public string NombreDocumento { get; set; }
    public string NombreArchivo { get; set; }
    public string ExtensionDocumento { get; set; }
    public string Sintesis { get; set; }
    public string SintesisHTML { get; set; }
    public long CreadorId { get; set; }
    public DateTime FechaAcuerdo { get; set; }
    public short TipoCuaderno { get; set; }
    public short Contenido { get; set; }
    public string IpUsuario { get; set; }
    public long UsuarioCaptura { get; set; }
    public int? SintesisOrden { get; set; }
    public int? AsuntoDocumentoId { get; set; }

    public List<PromocionAcuerdoDto> PromocionesDeterminacion { get; set; }
    public List<PromocionAcuerdoPersonasDto> PersonasNotificacionIndividual { get; set; }
    public List<PromocionAcuerdoAutoridadDto> AutoridadAsunto { get; set; }

    public long? AgendaId { get; set; }
    public int? ResultadoId { get; set; }
}

public class DatosDocumento
{

    public string NombreArchivo { get; set; }
    public int NumeroConsecutivo { get; set; }
    public int DocumentoId { get; set; }
    public string Mensaje { get; set; }
    public int AsuntoDocumentoId { get; set; }
    public int SintesisOrden { get; set; }
    public int CatOrganismoId { get; set; }
    public int NumeroOrden { get; set; }
    public string ExpedienteProcesado { get; set; }
    public Guid GuidDocumento { get; set; }

}