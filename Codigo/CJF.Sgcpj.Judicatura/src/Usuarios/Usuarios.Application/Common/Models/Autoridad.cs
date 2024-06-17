namespace CJF.Sgcpj.Judicatura.Usuarios.Application.Common.Models;
public class Autoridad
{
    public string Nombres { get; set; }
    public long AutoridadJudicialId { get; set; }
    public int? notiElect { get; set; }
    public string? usuarioRegistro {  get; set; }
    public string AutoridadJudicialDescripcion { get; set; }
    public int CatOrganismoId { get; set; }
    public string NombreOficial { get; set; }
}