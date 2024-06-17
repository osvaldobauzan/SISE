using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Comandos.SubirProyectoConAudienciaComando;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Models;

public class SubirProyectoConAudiencia
{
    public int CatOrganismoId { get; set; }

    public long AsuntoNeunId { get; set; }

    public long TitularId { get; set; }

    public long SecretarioId { get; set; }

    public int TipoSentenciaId { get; set; }

    public string? Sintesis { get; set; }

    public long EmpleadoId { get; set; }

    public string NombreArchivo { get; set; }

    public string IpUsuario { get; set; }

    public List<MotivosParteDto> Motivos { get; set; }
}
