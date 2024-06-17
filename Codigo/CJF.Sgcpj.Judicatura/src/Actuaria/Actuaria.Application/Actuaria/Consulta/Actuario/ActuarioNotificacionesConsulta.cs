using System.ComponentModel.DataAnnotations;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Actuario;
public class ActuarioNotificacionesConsulta : IRequest<ListaPaginada<ActuarioNotificacionesDto, ActuarioNotificacionesMetaDataEstadosDto>>
{
    [DataType(DataType.Date)]
    public DateTime? FechaInicial { get; init; }
    [DataType(DataType.Date)]
    public DateTime? FechaFinal { get; init; }
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
