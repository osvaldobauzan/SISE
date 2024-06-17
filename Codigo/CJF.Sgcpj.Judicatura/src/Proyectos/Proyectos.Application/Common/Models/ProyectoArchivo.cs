namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Models;

public class ProyectoArchivo
{
    public long pkProyecto { get; set; }

    public long pkProyectoArchivoId { get; set; }

    public string sNombreArchivo { get; set; }

    public string sAnioRuta { get; set; }

    public int CatOrganismoId { get; set; }

}
