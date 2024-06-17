namespace Proyectos.Domain.Entities;

public class MotivosParte
{
    public long IdParte { get; set; }

    public string Parte { get; set; }

    public int IdMotivo { get; set; }

    public string Motivo { get; set; }

    public int IdSentido { get; set; }

    public string sSentido { get; set; }

    public string Descripcion { get; set; }
}
