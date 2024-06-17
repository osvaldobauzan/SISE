namespace CJF.Sgcpj.Judicatura.Proyectos.Domain.Entities;

public class ProyectoConAudiencia
{
    public long pkProyectoId { get; set; }

    public int CatOrganismoId { get; set; }

    public long AsuntoNeunId { get; set; }

    public DateTime fFechaProyecto { get; set; }

    public long iTitular { get; set; }

    public long iSecretario { get; set; }

    public int iTipoSentenciaId { get; set; }

    public int iSentidoId { get; set; }

    public long fkProyectoVersionArchivoId { get; set; }

    public string sSintesis { get; set; }

    public int iVersion { get; set; }

    public int iEstado { get; set; }

    public string sNombreArchivo { get; set; }

    public int iRutaNas { get; set; }

    public string sRuta { get; set; }

    public string sAnioRuta { get; set; }

    public string UserNameTitular { get; set; }

    public string UserNameSecretario { get; set; }

    public string AsuntoAlias { get; set; }

    public string CatTipoAsunto { get; set; }
}
