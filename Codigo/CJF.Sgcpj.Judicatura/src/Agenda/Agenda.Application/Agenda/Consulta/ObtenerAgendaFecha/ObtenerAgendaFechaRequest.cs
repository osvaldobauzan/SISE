using MediatR;

namespace CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ObtenerAgendaFecha;
public class ObtenerAgendaFechaRequest : IRequest<List<ObtenerAgendaFechaDto>>
{
    public int CatIdOrganismo { get; set; }
    public DateTime? FechaIni { get; set; } 
    public DateTime? FechaFin { get; set; } 
    public string? Expediente { get; set; } 
    public string? Persona { get; set; } 

}
