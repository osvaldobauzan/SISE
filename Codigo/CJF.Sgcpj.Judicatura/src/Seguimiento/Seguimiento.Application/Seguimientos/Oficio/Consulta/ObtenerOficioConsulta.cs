using AutoMapper;
using MediatR;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Repositories;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Seguimientos.Oficio.Consulta;
public record class ObtenerOficioConsulta : IRequest<Common.Models.Oficio>
{
    public Common.Models.Oficio oficio { get; set; }
}


public class ObtieneOficioHandler : IRequestHandler<ObtenerOficioConsulta, Common.Models.Oficio>
{
    private readonly IOficioRepository _oficioRepository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;

    public ObtieneOficioHandler(IOficioRepository oficioRepository, IMapper mapper, ISesionService sesionService)
    {
        _oficioRepository = oficioRepository;
        _mapper = mapper;
        _sesionService = sesionService;
    }

    public async Task<Common.Models.Oficio> Handle(ObtenerOficioConsulta request, CancellationToken cancellationToken)
    {

        var Result = await _oficioRepository.getOficio(request.oficio);
        return Result;
    }

}

