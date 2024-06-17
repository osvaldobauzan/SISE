using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Actuario;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models.Actuario;
public class ConsultaPaginadaNotificaciones : IMapFrom<ActuarioNotificacionesConsulta>
{
    public string FechaInicial { get; init; }
    public string FechaFinal { get; init; }
    public int CatOrganismoId { get; set; }
    public int TamanioPagina { get; set; }
    public int NumeroPagina { get; set; }
    public string Texto { get; set; }
    public string OrdenarPor { get; set; }
    public bool? TipoOrden { get; set; }
    public int? FiltroTipo { get; set; }
    public int? FiltroTipoParteID { get; set; }
    public int? FiltroTipoNotificacionID { get; set; }
    public long? FiltroActuarioID { get; set; }
}
