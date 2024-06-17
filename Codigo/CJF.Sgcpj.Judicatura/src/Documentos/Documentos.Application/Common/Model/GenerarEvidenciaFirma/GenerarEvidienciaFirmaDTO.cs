using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documentos.Application.Common.Model.GenerarEvidenciaFirma;
public class GenerarEvidienciaFirmaDTO
{
    public string Contenido { get; set; }
    public byte[] ContenidoRaw { get; set; }
    public string PrimerFirmante { get; set; }
    public string Hash { get; set; }
    public string Fecha { get; set; }

}
