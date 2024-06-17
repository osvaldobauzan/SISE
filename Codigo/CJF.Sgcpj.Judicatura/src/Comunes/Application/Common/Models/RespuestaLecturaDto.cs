namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
public class RespuestasLecturaDto : RespuestasBaseDto
{
    public RespuestasLecturaDto()
    {
        Data = new RespuestasLecturaDataDto();
    }
    public RespuestasLecturaDataDto Data { get; set; }
}


