using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Documentos.Application.Common.Model.GenerarEvidenciaFirma;
public class DatosTSA
{
    public string SerialNumber { get; set; }
    public string Hash { get; set; }
    public String TimeStamp { get; set; }
    public string Subject { get; set; }
    public string Issuer { get; set; }
}
