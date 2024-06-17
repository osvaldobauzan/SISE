using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
public class AgregarCOEM
{
    public int NotElecId{ get; set; }
    public string Expediente{ get; set; }
	public int TipoComunicacion{ get; set; }
    public string NumeroOrigen{ get; set; }
	public DateTime FechaEnvio{ get; set; }
    public int Secretario{ get; set; }
	public string Mesa{ get; set; }
	public int TipoAsunto{ get; set; }
    public string NumeroExpedienteOrigen{ get; set; }
	public string Destinatario{ get; set; }
	public string Objetivo{ get; set; }
	public int OficinaCorrespondenciaComun{ get; set; }
    public int EmpleadoId { get; set; }
}
