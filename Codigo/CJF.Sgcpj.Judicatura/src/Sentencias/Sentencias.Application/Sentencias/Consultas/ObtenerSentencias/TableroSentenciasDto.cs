namespace CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Consultas.ObtenerSentencias;
public class TableroSentenciasDto
{
    public TableroSentenciasDto()
    {
        Datos = new List<SentenciaDto>();
    }

    public List<SentenciaDto> Datos { get; set; }
}
