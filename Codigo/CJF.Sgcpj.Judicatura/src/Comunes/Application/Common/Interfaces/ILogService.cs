using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;

public interface ILogService
{
    Task RegistrarEvento(DatosLog log);
}
