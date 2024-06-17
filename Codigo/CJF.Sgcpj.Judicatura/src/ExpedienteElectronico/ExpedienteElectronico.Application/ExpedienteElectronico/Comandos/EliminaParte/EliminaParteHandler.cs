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

namespace ExpedienteElectronico.Application.ExpedienteElectronico.Comandos.EliminaParte;
public class EliminaParteHandler: IRequestHandler<PersonaAsuntoDelete, bool>
{
    private readonly IExpedienteElectronicoRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    public EliminaParteHandler(IExpedienteElectronicoRepository repository, IMapper mapper, ISesionService sesionService)
    {
        _repository = repository;
        _mapper = mapper;
        _sesionService = sesionService;
    }
    public async Task<bool> Handle(PersonaAsuntoDelete request, CancellationToken cancellationToken)
    {
        request.UsuarioElimina = _sesionService.SesionActual.EmpleadoId;
        var datos = await _repository.EliminarPersonaAsunto(request);
        return datos;
    }
}
