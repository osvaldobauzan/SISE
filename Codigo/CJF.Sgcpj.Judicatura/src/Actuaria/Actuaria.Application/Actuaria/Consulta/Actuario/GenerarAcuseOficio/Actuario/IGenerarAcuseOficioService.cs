namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Actuario.GenerarAcuseOficio.Actuario;

public interface IGenerarAcuseOficioService
{
    Task<GenerarAcuseOficioResponseDto> GenerarAcuseOficioCodigoQr(GenerarAcuseOficioRequestDto request);
}