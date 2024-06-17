namespace CJF.Sgcpj.Judicatura.Promovente.Application.Common.Models;
public class AgregarPromovente
{
    public long AsuntoNeunId { get; set; }
    public int Tipo { get; set; }
    public string Nombre { get; set; }
    public string APaterno { get; set; }
    public string? AMaterno { get; set; }
    public int PersonaId { get; set; }
    public int RegistroEmpleadoId { get; set; }
    public int NumeroOrden { get; set; }
  
}