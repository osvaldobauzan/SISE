namespace CJF.Sgcpj.Judicatura.Application.Common.Models;
public class ListaDetallePromocionTablero<T, M>
{
    public List<T> Datos { get; set; }
    public List<M> Anexos { get; set; }
    public int Pagina { get; set; }
    public int TotalPaginas { get; set; }
    public int TotalRegistros { get; set; }
}
