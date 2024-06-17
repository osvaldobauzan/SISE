using CJF.Sgcpj.Judicatura.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.CalcularRegistro;
public class RegistroDto : IMapFrom<CalculaRegistro>
{
    public int Registro { get; set; }
}
