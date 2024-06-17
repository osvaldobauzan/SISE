namespace CJF.Sgcpj.Judicatura.Oficialia.Application.Common.Models;
public class FiltroPromociones<O, P, Q>
{
    public List<O> Secretario { get; set; }
    public List<P> Origen { get; set; }
    public List<Q> Capturo { get; set; }
}
