namespace CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;
public class FiltroTramite<O, P, Q, R, S, T, U>
{
    public List<O> Secretario {  get; set; }
    public List<P> Origen {  get; set; }
    public List<Q> TipoAsunto { get; set; }
    public List<R> Capturo {  get; set; }
    public List<S> Preautorizo { get; set; }
    public List<T> Autorizo { get; set; }
    public List<U> Cancelo{ get; set; }
}
