namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarSintesisManual;
public class AgregarSintesisManualDto
{
    public string Expediente { get; set; }
    public int TipoCuaderno { get; set; }
    public int ClasificacionCuaderno { get; set; }
    public DateTime FechaAuto { get; set; }
    public DateTime FechaPublicacion { get; set; }
    public long Titular { get; set; }
    public long Actuario { get; set; }
    public long AsuntoNeunId { get; set; }
    public int SintesisOrden { get; set; }
    public long UsuarioCaptura { get; set; }
    public string Sintesis { get; set; }
    public int CatOrganismoId { get; set; }

    public int Parte1 { get; set; }
    public int Parte2 { get; set; }
}
