namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
public class FiltroDetalleNotificaciones<O, P, Q>
{
    public List<O> TipoParte { get; set; }
    public List<P> TipoNotificacion { get; set; }
    public List<Q> Actuario { get; set; }
}
