using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models.Actuario;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using Microsoft.Extensions.Configuration;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Actuario.GenerarAcuseOficio.Actuario;
public class GenerarAcuseOficioService : IGenerarAcuseOficioService
{
    private readonly IConfiguration _configuration;
    private readonly IActuarioRepository _actuarioRepository;
    private readonly IServiceGeneracionOficio _serviceGeneracionOficio;

    public GenerarAcuseOficioService(IConfiguration configuration, IActuarioRepository actuarioRepository, IServiceGeneracionOficio serviceGeneracionOficio)
    {
        _configuration = configuration;
        _actuarioRepository = actuarioRepository;
        _serviceGeneracionOficio = serviceGeneracionOficio;
    }

    public async Task<GenerarAcuseOficioResponseDto> GenerarAcuseOficioCodigoQr(GenerarAcuseOficioRequestDto request)
    {
        var response = new GenerarAcuseOficioResponseDto();

        var oficiosActuario = await ObtenOficiosActuario(request);

        response.Mensaje = _serviceGeneracionOficio.ObtenDocumento(oficiosActuario);

        
        response.Status = !string.IsNullOrEmpty(response.Mensaje);

        return response;
    }

    private async Task<List<ConsultaOficioActuario>> ObtenOficiosActuario(GenerarAcuseOficioRequestDto request)
    {
        var response = await _actuarioRepository.ListaConsultaOficioPorActuario(request.AsuntoNeunId, request.AsuntoDocumentoId, request.ActuarioId, request.CatOrganismoId);

        return response is not null && response.Any() ? response : new List<ConsultaOficioActuario>();
    }
}
