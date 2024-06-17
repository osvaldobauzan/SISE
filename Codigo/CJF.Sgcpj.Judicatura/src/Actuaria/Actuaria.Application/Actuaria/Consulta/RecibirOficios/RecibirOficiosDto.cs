using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.RecibirOficios;

public class RecibirOficiosDto : IMapFrom<RecibirOficiosM>
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
    public bool Recibido { get;  set; }
    public string TipoNotificacion { get;  set; }
    public Guid? uGuid {  get; set; }
    public long? IdEmpleadoRecepcion { get; set; }
    public string? Nombre { get; set; }
    public string? ApellidoPaterno { get; set; }
    public string? ApellidoMaterno { get; set; }
}