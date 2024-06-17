using AutoMapper;
using MediatR;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Seguimientos.Oficio.Consulta;
public record class ObtenerOrdenOficioConsulta : IRequest<Common.Models.Oficio>
{
    public Common.Models.Oficio oficio { get; set; }
}


public class ObtieneOrdenOficioHandler : IRequestHandler<ObtenerOrdenOficioConsulta, Common.Models.Oficio>
{
    private readonly IOficioRepository _oficioRepository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;

    public ObtieneOrdenOficioHandler(IOficioRepository oficioRepository, IMapper mapper, ISesionService sesionService)
    {
        _oficioRepository = oficioRepository;
        _mapper = mapper;
        _sesionService = sesionService;
    }

    public async Task<Common.Models.Oficio> Handle(ObtenerOrdenOficioConsulta request, CancellationToken cancellationToken)
    {

        var Result = await _oficioRepository.getFolioOficio(request.oficio);
        return Result;
    }

}

