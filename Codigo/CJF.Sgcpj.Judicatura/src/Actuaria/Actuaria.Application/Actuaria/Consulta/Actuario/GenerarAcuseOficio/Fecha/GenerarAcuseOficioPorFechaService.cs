using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models.Actuario;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using Microsoft.Extensions.Configuration;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Actuario.GenerarAcuseOficio.Fecha;
public class GenerarAcuseOficioPorFechaService : IGenerarAcuseOficioPorFechaService
{
    private readonly IConfiguration _configuration;
    private readonly IActuarioRepository _actuarioRepository;
    private readonly IServiceGeneracionOficio _serviceGeneracionOficio;

    public GenerarAcuseOficioPorFechaService(IConfiguration configuration, IActuarioRepository actuarioRepository, IServiceGeneracionOficio serviceGeneracionOficio)
    {
        _configuration = configuration;
        _actuarioRepository = actuarioRepository;
        _serviceGeneracionOficio = serviceGeneracionOficio;
    }

    public async Task<GenerarAcuseOficioPorFechaResponseDto> GenerarAcuseOficioCodigoQr(GenerarAcuseOficioPorFechaRequestDto request)
    {
        var response = new GenerarAcuseOficioPorFechaResponseDto();

        var oficiosActuario = await ObtenOficiosActuario(request);

        response.Mensaje = _serviceGeneracionOficio.ObtenDocumentoPorFecha(oficiosActuario);

        response.Status = !string.IsNullOrEmpty(response.Mensaje);

        return response;
    }

    private async Task<List<ConsultaOficioActuario>> ObtenOficiosActuario(GenerarAcuseOficioPorFechaRequestDto request)
    {
        var response = await _actuarioRepository.ListaConsultaOficioPorActuarioPorFecha(request.ActuarioId, request.FechaInicio.ToString("yyyy-MM-dd"), 
            request.FechaFin.ToString("yyyy-MM-dd"), request.CatOrganismoId);

        return response is not null && response.Any() ? response : new List<ConsultaOficioActuario>();
    }
}
