using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Alertas;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;


namespace CJF.Sgcpj.Judicatura.Oficialia.Application.Seguimientos.Acuerdo.Consulta;
public record class ObtenerOrdenAcuerdoConsulta : IRequest<int>
{
    public Seguimiento.Application.Common.Models.Acuerdo acuerdo { get; set; }
}


public class ObtieneOrdenAcuerdoHandler : IRequestHandler<ObtenerOrdenAcuerdoConsulta, int>
{
    private readonly IAcuerdoRepository _acuerdoRepository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    private readonly IAlertsMessageService _alertsMessageService;
    private readonly IMediator _mediator;
    public ObtieneOrdenAcuerdoHandler(IAcuerdoRepository acuerdoRepository, IMapper mapper, ISesionService sesionService)
    {
        _acuerdoRepository = acuerdoRepository;
        _mapper = mapper;
        _sesionService = sesionService;
    }

    public async Task<int> Handle(ObtenerOrdenAcuerdoConsulta request, CancellationToken cancellationToken)
    {

        var Result = await _acuerdoRepository.getOrden(request.acuerdo);
        return Result;
    }

}

