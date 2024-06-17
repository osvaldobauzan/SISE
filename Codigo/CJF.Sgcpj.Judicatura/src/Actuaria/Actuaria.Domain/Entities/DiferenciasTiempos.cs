
namespace CJF.Sgcpj.Judicatura.Actuaria.Domain.Entities;
public class DiferenciasTiempos
{
    public string AsuntoAlias { get; set; }
    public long ActuarioId { get; set; }
    public int NumeroOrden { get; set; }
    public DateTime FechaAsigna { get; set; }
    public DateTime FechaNotifica { get; set; }
    public int DiferenciaDias { get; set; }
    public int DiaAsigna { get; set; } // Nuevo campo
    public int DiaNotifica { get; set; } // Nuevo campo
}
