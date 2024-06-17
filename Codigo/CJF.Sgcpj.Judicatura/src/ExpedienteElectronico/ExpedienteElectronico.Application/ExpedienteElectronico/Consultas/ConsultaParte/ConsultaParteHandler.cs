using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Repositories;
using ExpedienteElectronico.Application.Common.Models;
using MediatR;

namespace ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.ConsultaParte;
public class ConsultaParteHandler : IRequestHandler<PersonaAsuntoFiltro, PersonaAsuntoDTO>
{
    private readonly IExpedienteElectronicoRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    public ConsultaParteHandler(IExpedienteElectronicoRepository repository, IMapper mapper, ISesionService sesionService)
    {
        _repository = repository;
        _mapper = mapper;
        _sesionService = sesionService;
    }
    public async Task<PersonaAsuntoDTO> Handle(PersonaAsuntoFiltro request, CancellationToken cancellationToken)
    {
        var datos = await _repository.ObtenerPersonaAsunto(request);
        return datos;
    }
}
