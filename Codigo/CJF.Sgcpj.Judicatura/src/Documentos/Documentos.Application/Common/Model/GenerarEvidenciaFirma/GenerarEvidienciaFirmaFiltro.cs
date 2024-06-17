using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Documentos.Application.Common.Model.GenerarEvidenciaFirma;
public class GenerarEvidienciaFirmaFiltro: IRequest<GenerarEvidienciaFirmaDTO>
{
    public string p7m { get; set; }
    public string ArchivoFirmado { get;set; }
    public string GuidDocumento { get; set; }
}
