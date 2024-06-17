namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Actuario.GenerarAcuseOficio.Fecha;

public interface IGenerarAcuseOficioPorFechaService
{
    Task<GenerarAcuseOficioPorFechaResponseDto> GenerarAcuseOficioCodigoQr(GenerarAcuseOficioPorFechaRequestDto request);
}