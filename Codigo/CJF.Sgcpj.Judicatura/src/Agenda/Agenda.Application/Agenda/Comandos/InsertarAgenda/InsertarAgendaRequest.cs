using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Comandos.InsertarAgenda;
public class InsertarAgendaRequest : IRequest<ResultadoInsertarAgendaDto>
{
    public int NumeroNeun { get; set; }
    public string Expendiente { get; set; }
    public int IdTipoAsunto { get; set; }
    public int IdTipoAudiencia { get; set; }
    public int NumeroCaptura { get; set; }
    public int PartSel { get; set; }
    public int UsaPartes { get; set; }
    public int SecretarioId { get; set; }
    public int? AudienciaOraltis { get; set; }
    public int? IdAudienciaOraltis { get; set; }
    public string NombreSolicitante { get; set; }
    public string MotivoConsulta { get; set; }
    public DateTime FechaAudiencia { get; set; }
    public string HoraAudiencia { get; set; }
    public DateTime FechaAcuerdoSolicitud { get; set; } 
}
