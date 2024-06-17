using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ValidarExpediente;

public class ValidarExpedienteConsultaHandler : IRequestHandler<ValidarExpedienteConsulta, ValidacionExpedienteDto>
{
    private readonly IProyectosRepository _repository;
    private readonly ISesionService _sessionService;
    private readonly ILogger<ValidarExpedienteConsultaHandler> _logger;

    public ValidarExpedienteConsultaHandler(IProyectosRepository repository, ISesionService sesionService, ILogger<ValidarExpedienteConsultaHandler> logger)
    {
        _repository = repository;
        _sessionService = sesionService;
        _logger = logger;
    }

    public async Task<ValidacionExpedienteDto> Handle(ValidarExpedienteConsulta request, CancellationToken cancellationToken)
    {
        try
        {
            var validarExpediente = new Common.Models.ValidarExpediente
            {
                AsuntoNeunId = request.AsuntoNeunId,
                CatCuadernoId = request.CatCuadernoId,
                CatOrganismoId = request.CatOrganismoId,
                CatTipoAsuntoId = request.CatTipoAsuntoId,
                NumeroExpediente = request.NumeroExpediente
            };

            validarExpediente.CatOrganismoId = _sessionService.SesionActual.CatOrganismoId;
            var result = await _repository.ValidarExpediente(validarExpediente);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
