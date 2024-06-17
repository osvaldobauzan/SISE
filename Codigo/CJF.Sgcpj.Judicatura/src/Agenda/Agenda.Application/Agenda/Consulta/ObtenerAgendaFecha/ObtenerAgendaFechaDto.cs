using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Css.Values;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ObtenerAgendaFecha;
public class ObtenerAgendaFechaDto
{
    public int IdNeun { get; set; }
    public string TipoAsunto { get; set; }
    public string Expediente { get; set; }
    public string Audiencia { get; set; }
    public int IdTipoAudiencia { get; set; }    
    public int IdAgenda { get; set; }
    public string Parte { get; set; }
    public string FechaAudiencia { get; set; }
    public string HoraAudiencia { get; set; }
    public string Resultado { get; set; }
    public int IdResultado { get; set; }
    public string Empleado { get; set; }
    public string Secretario { get; set; }

}
