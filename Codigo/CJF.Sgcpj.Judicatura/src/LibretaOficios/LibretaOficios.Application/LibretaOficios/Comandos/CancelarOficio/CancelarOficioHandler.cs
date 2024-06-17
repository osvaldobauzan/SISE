using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.LibretaOficios.Application.Common.Models;
using CJF.Sgcpj.Judicatura.LibretaOficios.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.LibretaOficios.Application.LibretaOficios.Comandos.CancelarOficio;
public class CancelarOficioHandler : IRequestHandler<CancelarOficioFiltro, bool>

{
    private readonly ILibretaOficiosRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;

    public CancelarOficioHandler(ILibretaOficiosRepository repository, IMapper mapper, ISesionService sesionService)
    {
        _repository = repository;
        _mapper = mapper;
        _sesionService = sesionService;
    }
    public async Task<bool> Handle(CancelarOficioFiltro request, CancellationToken cancellationToken)
    {
        request.CatOrganismoId = _sesionService.SesionActual.CatOrganismoId;
        request.EmpleadoId = _sesionService.SesionActual.EmpleadoId;
        var dato = await _repository.CancelarOficio(request);
        return dato;
    }
}
