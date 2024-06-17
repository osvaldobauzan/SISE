namespace CJF.Sgcpj.Judicatura.Application.Common.Models;
public class CatalogoCuaderno
{
    public int CuadernoId { get; set; }
    public string Cuaderno { get; set; }
    public string Color { get; set; }
    public int ClasificacionCuadernoId { get; set; }
    public string ClasificacionCuaderno { get; set; }
    public string CuadernoCorto { get; set; }
}
