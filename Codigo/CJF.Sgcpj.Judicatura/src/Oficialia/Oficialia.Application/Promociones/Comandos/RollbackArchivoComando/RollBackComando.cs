using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.RollBackAnexo;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.RollbackArchivoComando;
public class RollBackComando : IRequest<long>
{
    public RollBackAnexoDto Anexo { get; set; }
}
public class RollBackComandoHandler : IRequestHandler<RollBackComando, long>
{
    private readonly IMapper _mapper;
    private readonly IPromocionesRepository _promocionesRepository;
    private readonly ISesionService _sesionService;

    public RollBackComandoHandler(IMapper mapper, IPromocionesRepository promocionesRepository, ISesionService sesionService)
    {
        _mapper = mapper;
        _promocionesRepository = promocionesRepository;
        _sesionService = sesionService;
    }
    public async Task<long> Handle(RollBackComando request, CancellationToken cancellationToken)
    {
        request.Anexo.CatIdOrganismo = _sesionService.SesionActual.CatOrganismoId;
        request.Anexo.RegistroEmpleadoId = _sesionService.SesionActual.EmpleadoId;
        var anexo = _mapper.Map<Common.Models.RollBackAnexo>(request.Anexo);
        return await _promocionesRepository.RollBackAnexo(anexo);
    }
}