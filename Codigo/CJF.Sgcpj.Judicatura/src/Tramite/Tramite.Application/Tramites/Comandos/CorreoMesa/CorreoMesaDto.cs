using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Comandos.CorreoMesa;
public class CorreoMesaDto : IMapFrom<Judicatura.Common.Application.Common.Models.CorreoMesa>
{
    public long EmpleadoId { get; set; }
    public string Nombre { get; set; }
    public string Correo { get; set; }
}
