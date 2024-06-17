using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Repositories;
using ExpedienteElectronico.Application.Common.Models;
using MediatR;

namespace ExpedienteElectronico.Application.ExpedienteElectronico.Comandos.ActualizaParte;
public class ActualizaParteHandler: IRequestHandler<PersonaAsuntoUpdate, bool>
{
    private readonly IExpedienteElectronicoRepository _repository;
    private readonly ISesionService _sesionService;
    public ActualizaParteHandler(IExpedienteElectronicoRepository repository, ISesionService sesionService)
    {
        _repository = repository;
        _sesionService = sesionService;
    }
    public async Task<bool> Handle(PersonaAsuntoUpdate request, CancellationToken cancellationToken)
    {
        request.UsuarioCaptura = _sesionService.SesionActual.EmpleadoId;
        var datos = await _repository.ActualizarPersonaAsunto(request);
        return datos;
    }
}
