using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
public class ObtenerAcuerdoM
{
    public long AsuntoNeunId { get; set; }
    public long OrganismoId { get; set; }
    public long SintesisOrdenId { get; set; }
    public long QuejosoId { get; set; }
    public string Quejoso { get; set; }
    public bool QuejosoOtros { get; set; }
    public long AutoridadId { get; set; }
    public string Autoridad { get; set; }
    public bool AutoridadOtros { get; set; }
    public DateTime? FechaAuto { get; set; }
    public string Sintesis { get; set; }
    public DateTime? FechaPublicacion { get; set; }
    public long TitularId { get; set; }
    public string Titular { get; set; }
    public long ActuarioId { get; set; }
    public string Actuario { get; set; }
    public long ClasificacionCuadernoId { get; set; }
    public string ClasificacionCuaderno { get; set; }
    public long CuadernoId { get; set; }
    public string Cuaderno { get; set; }
    public int ActivoSintesis { get; set; }
    public long EmpleadoIdSintesis { get; set; }
    public string AsuntoAlias { get; set; }
    public int TipoOrigen { get; set; }
    public long NumeroAlias { get; set; }
    public long TipoAsuntoId { get; set; }
    public string TipoAsunto { get; set; }
    public string Color { get; set; }
    public string NombreArchivo { get; set; }
    public int ActivoRegistroSintesis { get; set; }
    public long AutorizacionAcuerdoId { get; set; }
    public string AutorizacionAcuerdo { get; set; }
    public string SintesisHTML { get; set; }
    public long AsuntoDocumentoId { get; set; }
    public string NombreDocumento { get; set; }
    public string ExtensionDocumento { get; set; }
    public long EmpladoIdCreaDocumentoId { get; set; }
    public string EmpladoCreaDocumento { get; set; }
    public string nombreCorto { get; set; }
    public string EmpleadoSintesis { get; set; }
    public string Expediente { get; set; }
    public bool SintesisTramite { get; set; }
    public long fkIdUsuario { get; set; }
    public int AceptaOponePublicarDatos { get; set; }
}
