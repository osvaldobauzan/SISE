using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarCOE
{
public class AgregarCOEDto : IMapFrom<AgregarCOEM>
    {
        public int NotElecId { get; set; }
        public string Expediente { get; set; }
        public int TipoComunicacion { get; set; }
        public string NumeroOrigen { get; set; }
        public DateTime FechaEnvio { get; set; }
        public int Secretario { get; set; }
        public string Mesa { get; set; }
        public int TipoAsunto { get; set; }
        public string NumeroExpedienteOrigen { get; set; }
        public string Destinatario { get; set; }
        public string Objetivo { get; set; }
        public int OficinaCorrespondenciaComun { get; set; }
        public int EmpleadoId { get; set; }
    }
}
