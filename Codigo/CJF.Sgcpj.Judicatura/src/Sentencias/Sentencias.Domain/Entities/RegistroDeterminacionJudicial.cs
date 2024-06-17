namespace CJF.Sgcpj.Judicatura.Sentencias.Domain.Entities;

public class RegistroDeterminacionJudicial
{
    public string NombreArchivo { get; set; }

    public int IdEstatus { get; set; }

    public string Ip { get; set; }

    public string Observaciones { get; set; }

    public long AsuntoNeunId { get; set; }

    public int NumeroOrden { get; set; }

    public int SintesisOrden { get; set; }

    public int Fojas { get; set; }

    public string ArchivoExtension { get; set; }
}
