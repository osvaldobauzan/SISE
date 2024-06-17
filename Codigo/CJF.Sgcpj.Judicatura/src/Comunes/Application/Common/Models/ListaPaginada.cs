namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
public class ListaPaginada<T,M>
{

    public List<T> Datos { get; set; }

    public M MetaDatos { get; set; }

    public int Pagina { get; set; }
    public int TotalPaginas { get; set; }
    public int TotalRegistros { get; set; }
}
