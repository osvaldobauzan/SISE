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
public record class ObtenerComboPartes : IRequest<List<Common.Models.Seguimiento>>
{
    public Common.Models.Seguimiento seguimiento { get; set; }
}


public class ObtieneComboPartesHandler : IRequestHandler<ObtenerComboPartes, List<Common.Models.Seguimiento>>
{
    private readonly ISeguimientoRepository _seguimientoRepository;
    private readonly ISesionService _sesionService;

    public ObtieneComboPartesHandler(ISeguimientoRepository seguimientoRepository, ISesionService sesionService)
    {
        _seguimientoRepository = seguimientoRepository;
        _sesionService = sesionService;
    }

    public async Task<List<Common.Models.Seguimiento>> Handle(ObtenerComboPartes seguimiento, CancellationToken cancellationToken)
    {
        seguimiento.seguimiento.CatOrganismoId = _sesionService.SesionActual.CatOrganismoId;

        return await _seguimientoRepository.getCombPartes(seguimiento.seguimiento);
    }

}

