using CJF.Sgcpj.Judicatura.Sentencias.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Sentencias.Application.FirmadorDocumentos.Consultas.LecturaArchivoFirmador;
public class RespuestaLecturaDto : RespuestaBaseDto
{
    public RespuestaLecturaDto()
    {
        Data = new RespuestaLecturaDataDto();
    }
    public RespuestaLecturaDataDto Data { get; set; }
}
