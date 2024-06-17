using System.Text.Json.Serialization;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerArchivos;
public class ArchivosPromocionDto
{
    public ArchivosPromocionDto()
    {
        Archivos = new List<Documento>();
        Anexos = new List<Documento>();
    }
    public List<Documento> Archivos { get; set; }
    public List<Documento> Anexos { get; set; }
}

public class Documento
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public Guid GuidDocumento { get; set; }
}
