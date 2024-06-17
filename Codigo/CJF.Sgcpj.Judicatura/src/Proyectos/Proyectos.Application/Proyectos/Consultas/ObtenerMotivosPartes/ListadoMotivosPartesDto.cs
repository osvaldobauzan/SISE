namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerMotivosPartes;

public class ListadoMotivosPartesDto
{
    public List<MotivosParteDto> Motivos { get; set; }
}


public class MotivosParteDto
{
    public long IdParte { get; set; }

    public string Parte { get; set; }

    public int IdMotivo { get; set; }

    public string Motivo { get; set; }

    public int IdSentido { get; set; }

    public string Sentido { get; set; }

    public string Descripcion { get; set; }
}