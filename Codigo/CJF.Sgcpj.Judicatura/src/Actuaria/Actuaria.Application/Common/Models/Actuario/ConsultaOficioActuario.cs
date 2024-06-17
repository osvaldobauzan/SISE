namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models.Actuario;
public class ConsultaOficioActuario
{
    public string? CatOrganismo { get; set; }
    public string? Expediente { get; set; }
    public string? TipoAsunto { get; set; }
    public string? Parte { get; set; }
    public string? Caracter { get; set; }
    public string? TipoPersona { get; set; }
    public int Folio { get; set; }
    public int Año { get; set; }
    public DateTime Fecha { get; set; }
    public int CatOrganismoIdUnc { get; set; }
    public long AsuntoNeunId { get; set; }
    public Int16 AnexoTipoId { get; set; }
}

