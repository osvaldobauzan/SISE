namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.GenerarOficio;
public class PersonasDto // : IMapFrom<ParteNotificacionFolios>
{
    public int PersonaId { get; set; } //considerada
    public int PromoventeId { get; set; }
    public int? TipoNotificacionId { get; set; } //considerada
    public int? TipoAnexoId { get; set; } //considerada
    public int? NumIntentosNotificacion { get; set; }
    public string? TextoOficioLibre { get; set; } //considerada
    public string? NombreParte { get; set; } //considerada
    public string? DescripcionPromovente { get; set; }
    public string? AnexoParteDescripcion { get; set; }
}