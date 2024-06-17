namespace CJF.Sgcpj.Judicatura.Sentencias.Domain.Entities;

public class RegistroBitacora
{
    public int CatOrganismoId { get; set; }

    public long AsuntoNeunId { get; set; }

    public string TamanioArcivo { get; set; }

    public string Carpeta { get; set; }

    public string NombreArchvo { get; set; }

    public DateTime FechaInicia { get; set; }

    public string IpHost { get; set; }

    public string IpCliente { get; set; }
}
