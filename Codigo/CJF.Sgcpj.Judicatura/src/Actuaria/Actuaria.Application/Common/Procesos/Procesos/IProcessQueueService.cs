

using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.GenerarOficio;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Procesos;
public interface IProcessQueueService
{
    Task<(bool IsSuccess, string? ErrorMessage)> TriggerProcessAsync(CrearOficiosDTO oficiosMessage);
}