using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
public interface ISesionService
{
    Sesion SesionActual { get; }
}
