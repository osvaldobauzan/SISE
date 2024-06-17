namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
public class AgregarActuarioM
{
    public long AsuntoNeunId { get; set; }
    public int AsuntoId { get; set; }
    public long ActuarioId { get; set; }
    public long Parte { get; set; }
    public long Promovente { get; set; }
    public int TipoNotificacionId { get; set; }
    public bool TieneCOE { get; set; }
    public long NotElecId { get; set; }
    public long EmpleadoId { get; set; }

    public object? generarOficio { get; set; }
}
