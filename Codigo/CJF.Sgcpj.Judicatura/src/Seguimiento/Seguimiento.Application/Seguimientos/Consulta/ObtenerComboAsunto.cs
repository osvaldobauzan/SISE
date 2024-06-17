using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;
using MediatR;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;


namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Seguimientos.Consulta;
public record class ObtenerComboAsunto : IRequest<List<Common.Models.Seguimiento>>
{
    public Common.Models.Seguimiento seguimiento { get; set; }
}


public class ObtieneComboXAsuntoHandler : IRequestHandler<ObtenerComboAsunto, List<Common.Models.Seguimiento>>
{
    private readonly ISeguimientoRepository _seguimientoRepository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;

    public ObtieneComboXAsuntoHandler(ISeguimientoRepository seguimientoRepository, IMapper mapper, ISesionService sesionService)
    {
        _seguimientoRepository = seguimientoRepository;
        _mapper = mapper;
        _sesionService = sesionService;
    }

    public async Task<List<Common.Models.Seguimiento>> Handle(ObtenerComboAsunto seguimiento, CancellationToken cancellationToken)
    {
       
        var Result = await _seguimientoRepository.getCombAsunto(seguimiento.seguimiento);
        return Result;
    }

}

