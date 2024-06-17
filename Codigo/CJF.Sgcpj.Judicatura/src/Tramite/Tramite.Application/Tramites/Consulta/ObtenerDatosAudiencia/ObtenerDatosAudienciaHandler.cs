using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerDatosAudiencia;
public class ObtenerDatosAudienciaHandler : IRequestHandler<ObtenerDatosAudienciaFiltro, DatosAudienciaDTO>
{
    private readonly ITramitesRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    public ObtenerDatosAudienciaHandler(ITramitesRepository repository, IMapper mapper, ISesionService sesionService)
    {
        _repository = repository;
        _mapper = mapper;
        _sesionService = sesionService;
    }
    public async Task<DatosAudienciaDTO> Handle(ObtenerDatosAudienciaFiltro request, CancellationToken cancellationToken)
    {
        DatosAudienciaDTO audiencia = new DatosAudienciaDTO();
        var datos = await _repository.ObtenerDatosAudiencia(request);
         audiencia.TiposAsuntos = datos.Item1;
        audiencia.TiposAudiencias = datos.Item2;
        audiencia.Resultados = datos.Item3;
        audiencia.Audiencias = datos.Item4;
        
        return audiencia;
    }
}
