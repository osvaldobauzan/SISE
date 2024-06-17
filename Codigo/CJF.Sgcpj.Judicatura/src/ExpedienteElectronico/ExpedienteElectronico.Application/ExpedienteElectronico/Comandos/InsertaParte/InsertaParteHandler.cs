using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Repositories;
using ExpedienteElectronico.Application.Common.Models;
using MediatR;

namespace ExpedienteElectronico.Application.ExpedienteElectronico.Comandos.InsertaParte;
public class InsertaParteHandler : IRequestHandler<PersonaAsuntoInsert, Int64>
{
    private readonly IExpedienteElectronicoRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    public InsertaParteHandler(IExpedienteElectronicoRepository repository, IMapper mapper, ISesionService sesionService)
    {
        _repository = repository;
        _mapper = mapper;
        _sesionService = sesionService;
    }
    public async Task<Int64> Handle(PersonaAsuntoInsert request, CancellationToken cancellationToken)
    {
        request.UsuarioCaptura = _sesionService.SesionActual.EmpleadoId;
        var datos = await _repository.InsertarPersonaAsunto(request);
        return datos;
    }
}
