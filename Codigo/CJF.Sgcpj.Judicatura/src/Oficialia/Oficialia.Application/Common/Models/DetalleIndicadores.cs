
namespace CJF.Sgcpj.Judicatura.Oficialia.Application.Common.Models;
public class DetalleIndicadores
{
    public long EmpleadoId { get; set; }
    public string? NombreOficial { get; set; }
    public string? UserName { get; set; }
    public int TotalPromociones { get; set; }
    public int PromocionesTurnadas { get; set; }
    public int TotalPromocionesAnoActual { get; set; }

    private double _promedioPromocionesTurnadasPorDia;

    public double PromedioPromocionesTurnadasPorDia
    {
        get => Math.Round(_promedioPromocionesTurnadasPorDia, 2);
        set => _promedioPromocionesTurnadasPorDia = value;
    }
    public double TiempoPromedioMins { get; set; }
}
