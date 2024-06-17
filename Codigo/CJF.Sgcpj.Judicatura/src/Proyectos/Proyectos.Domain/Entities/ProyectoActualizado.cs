namespace CJF.Sgcpj.Judicatura.Proyectos.Domain.Entities;

public class ProyectoActualizado
{
    public long AsuntoNeunId { get; set; }

    public string AsuntoAlias { get; set; }

    public int CatOrganismoId { get; set; }

    public string CatOrganismo { get; set; }

    public int CatTipoAsuntoId { get; set; }

    public string CatTipoAsunto { get; set; }

    public int TipoCuaderno { get; set; }

    public string sTipoCuaderno { get; set; }

    public long pkProyectoId { get; set; }

    public int iEstado { get; set; }

    public long pkProyectoArchivoId { get; set; }

    public string sNombreArchivo { get; set; }

    public string sAnioRuta { get; set; }

    public string UserNameTitular { get; set; }

    public string UserNameSecretario { get; set; }

    public long iTitular { get; set; }

    public long iSecretario { get; set; }

    public int iVersion { get; set; }

    public DateTime fFechaAlta { get; set; }
}
