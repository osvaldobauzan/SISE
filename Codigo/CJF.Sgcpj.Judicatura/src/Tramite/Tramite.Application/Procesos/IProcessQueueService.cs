using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Comandos.CreacionOficiosComando;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Procesos;
public interface IProcessQueueService
{
    Task<(bool IsSuccess, string? ErrorMessage)> TriggerProcessAsync(CrearOficiosDTO oficiosMessage);
}
