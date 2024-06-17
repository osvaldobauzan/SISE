using AutoMapper;
using Azure.Data.Tables;
using Azure.Identity;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.LibretaOficios.Application.Common.Models;
using CJF.Sgcpj.Judicatura.LibretaOficios.Application.Common.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.LibretaOficios.Application.LibretaOficios.Consultas;
public class ObtenerLibretaOficioHandler : IRequestHandler<LibretaOficioFiltro, List<LibretaOficio>>

{
    private readonly ILibretaOficiosRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;


    public ObtenerLibretaOficioHandler(ILibretaOficiosRepository repository, IMapper mapper, ISesionService sesionService)
    {
        _repository = repository;
        _mapper = mapper;
        _sesionService = sesionService;
    }
    public async Task<List<LibretaOficio>> Handle(LibretaOficioFiltro request, CancellationToken cancellationToken)
    {
        request.CatOrganismoId = _sesionService.SesionActual.CatOrganismoId;
        request.CantidadRegistros = request.NoRegistros == 0 ? int.MaxValue : request.NoRegistros;
        List<LibretaOficio> datos = await _repository.ObtenerLibretaOficio(request);
        return datos;
    }
}

