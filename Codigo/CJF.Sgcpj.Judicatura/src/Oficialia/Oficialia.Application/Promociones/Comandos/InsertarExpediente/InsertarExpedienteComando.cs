using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.InsertarExpediente;
public class InsertarExpedienteComando:IRequest<ResultadoInsertarExpedienteDto>
{

    public InsertarExpedienteDto Expediente { get; set; }
}
public class InsertarExpedienteComandoHandler : IRequestHandler<InsertarExpedienteComando, ResultadoInsertarExpedienteDto>
{
    private readonly IMapper _mapper;
    private readonly IPromocionesRepository _promocionesRepository;
    private readonly ISesionService _sesionService;

    public InsertarExpedienteComandoHandler(IMapper mapper, IPromocionesRepository promocionesRepository, ISesionService sesionService)
    {
        _mapper = mapper;
        _promocionesRepository = promocionesRepository;
        _sesionService = sesionService;
    }
    public async Task<ResultadoInsertarExpedienteDto> Handle(InsertarExpedienteComando request, CancellationToken cancellationToken)
    {
        ResultadoInsertarExpedienteDto resultado = new ResultadoInsertarExpedienteDto();
        request.Expediente.CatOrganismoId = _sesionService.SesionActual.CatOrganismoId;
        request.Expediente.EmpleadoId = _sesionService.SesionActual.EmpleadoId;
        var expediente = _mapper.Map<Common.Models.InsertarExpediente>(request.Expediente);
        var expedienteRes = await _promocionesRepository.InsertarExpediente(expediente);
        resultado.AsuntoNeunId = expedienteRes.AsuntoNeunId;
        resultado.AsuntoId = expedienteRes.AsuntoId;

        return resultado;
    }
}
