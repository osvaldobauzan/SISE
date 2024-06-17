using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Models;
public class InfoDocumentos
{
    public bool Firmado { get; set; }
    public int EsAcuerdo { get; set; }
    public string Ruta { get; set; }
    public string NombreArchivo { get; set; }
    public string ExtensionDocumento { get; set; }
    public Guid uGuid { get; set; }

}
