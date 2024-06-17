namespace CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Consultas.ObtenerSentencias;

public class ExpedienteDto
{
    public int AsuntoNeunId { get; set; }

    public string AsuntoAlias { get; set; }

    public int CatOrganismoId { get; set; }

    public string CatTipoAsunto { get; set; }

    public int CatTipoAsuntoId { get; set; }

    public string TipoProcedimiento { get; set; }

    public string CatOrganismo { get; set; }

    public int TipoCuadernoId { get; set; }

    public string Cuaderno { get; set; }

    public int? AsuntoDocumentoId { get; set; }
    public string GuidDocumento { get; set; }
}
