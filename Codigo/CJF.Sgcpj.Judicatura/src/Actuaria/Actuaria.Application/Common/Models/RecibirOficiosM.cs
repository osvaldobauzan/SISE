using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
public class RecibirOficiosM
{
    public string Expediente { get; set; }
    public string TipoAsuntoDescripcion { get; set; }
    public int ConArchivo { get; set; }
    public int Folio { get; set; }
    public long AsuntoNeunId { get; set; }
    public int CatTipoAsuntoId { get; set; }
    public int CatOrganismoId { get; set; }
    public string NombreTipoCuaderno { get; set; }
    public int TipoCuaderno { get; set; }
    public int AnexoId { get; set; }
    public string TipoNotificacion { get; set; }
    public Guid? uGuid { get; set; }
    public long? IdEmpleadoRecepcion { get; set; }
    public string? Nombre { get; set; }
    public string? ApellidoPaterno { get; set; }
    public string? ApellidoMaterno { get; set; }

}
