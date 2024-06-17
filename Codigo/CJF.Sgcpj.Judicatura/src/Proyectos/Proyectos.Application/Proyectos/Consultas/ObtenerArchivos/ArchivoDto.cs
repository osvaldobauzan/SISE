namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerArchivos;

public class ArchivoDto
{
    public long AsuntoNeunId { get; set; }

    public long pkProyectoArchivoId { get; set; }

    public string sNombreArchivo { get; set; }

    public string sNombreArchivoReal { get; set; }

    public string sAnioRuta { get; set; }

    public int CatOrganismoId { get; set; }

    public string UserNameTitular { get; set; }

    public string UserNameSecretario { get; set; }

    public DateTime fFechaAlta { get; set; }
}
