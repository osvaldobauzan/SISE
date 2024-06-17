﻿namespace CJF.Sgcpj.Judicatura.Tramite.Domain.Entities;

public class Tramite
{
    public Tramite() { }
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
    public int SintesisOrden { get; set;}
    public string TipoProcedimiento{ get; set; }
    public Guid GuidDocumento { get; set; }
    public Expediente Expediente { get; set; }
    public bool? EsPromocionE { get; set; }
    public int? OficiosFirmados { get; set; }
    public int? CanceloCuenta { get; set; }
    public bool? PromocionCompleta { get; set; }
}
