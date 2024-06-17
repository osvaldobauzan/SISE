namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
public class ListaDetalleAcuerdo<T,M>
{
    public List<T> Datos { get; set; }
    public List<M> Promociones { get; set; }
}
