using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.RecibirOficios;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.RecibirOficios;
public class RecibirOficiosComandoHandler : IRequestHandler<RecibirOficiosComando, List<RecibirOficiosDto>>
{
    private readonly IActuariaRepository _actuariaRepository;
    private readonly ISesionService _sesionService;
    private readonly IMapper _mapper;

    public RecibirOficiosComandoHandler(IActuariaRepository actuariaRepository, ISesionService sesionService, IMapper mapper)
    {
        _actuariaRepository = actuariaRepository;
        _sesionService = sesionService;
        _mapper = mapper;
    }
    public async Task<List<RecibirOficiosDto>> Handle(RecibirOficiosComando request, CancellationToken cancellationToken)
    {
        var idEmpleado = _sesionService.SesionActual.EmpleadoId;
        var catOrganismoId = _sesionService.SesionActual.CatOrganismoId;
        foreach (var item in request.Oficios)
        {
            try
            {
                var oficioType = new OficiosType();
                oficioType.AnexoId = item.AnexoId;
                oficioType.AsuntoNeunId = item.AsuntoNeunId;                            
                oficioType.Folio = item.Folio.ToString();

                await _actuariaRepository.RecibirOficios(catOrganismoId, idEmpleado, oficioType);
                item.Recibido = true;
            }
            catch (Exception ex)
            {
                item.Recibido = false;

            }
        }


        return request.Oficios;
    }
}
