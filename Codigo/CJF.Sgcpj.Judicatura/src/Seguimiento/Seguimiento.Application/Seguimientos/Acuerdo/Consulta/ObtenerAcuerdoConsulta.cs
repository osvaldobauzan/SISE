using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Alertas;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Repositories;
using MediatR;


namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Seguimientos.Acuerdo.Consulta;
public record class ObtenerAcuerdoConsulta : IRequest<Common.Models.Acuerdo>
{
    public Common.Models.Acuerdo acuerdo { get; set; }
}


public class ObtieneAcuerdoHandler : IRequestHandler<ObtenerAcuerdoConsulta, Common.Models.Acuerdo>
{
    private readonly IAcuerdoRepository _acuerdoRepository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;    
    private readonly IAlertsMessageService _alertsMessageService;
    private readonly IMediator _mediator;

    public ObtieneAcuerdoHandler(IAcuerdoRepository acuerdoRepository, IMapper mapper, ISesionService sesionService)
    {
        _acuerdoRepository = acuerdoRepository;
        _mapper = mapper;
        _sesionService = sesionService;
    }

    public async Task<Common.Models.Acuerdo> Handle(ObtenerAcuerdoConsulta request, CancellationToken cancellationToken)
    {

        var Result = await _acuerdoRepository.getAcuerdo(request.acuerdo);
        return Result;
    }

}

