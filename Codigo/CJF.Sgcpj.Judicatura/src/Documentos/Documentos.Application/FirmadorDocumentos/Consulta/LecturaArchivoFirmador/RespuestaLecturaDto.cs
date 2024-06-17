using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Documentos.Application.Common.Model;

namespace Documentos.Application.FirmadorDocumentos.Consulta.LecturaArchivoFirmador;
public class RespuestaLecturaDto : RespuestaBaseDto
{
    public RespuestaLecturaDto()
    {
        Data = new RespuestaLecturaDataDto();
    }
    public RespuestaLecturaDataDto Data { get; set; }
}


