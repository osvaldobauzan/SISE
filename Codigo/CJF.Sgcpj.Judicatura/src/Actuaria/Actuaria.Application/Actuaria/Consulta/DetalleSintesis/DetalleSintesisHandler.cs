using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DetalleSintesis;
public class DetalleSintesisHandler : IRequestHandler<FiltroDetalleSintesis, List<DetalleSintesisDTO>>

{
    private readonly IActuariaRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;


    public DetalleSintesisHandler(IActuariaRepository repository, IMapper mapper, ISesionService sesionService)
    {
        _repository = repository;
        _mapper = mapper;
        _sesionService = sesionService;
    }
    public async Task<List<DetalleSintesisDTO>> Handle(FiltroDetalleSintesis request, CancellationToken cancellationToken)
    {
        List<DetalleSintesisDTO> datos = await _repository.ObtenerDetalleSintesis(request);
        return datos;
    }
}

