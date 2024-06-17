using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarCOE.Acuse;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Extensions.Logging;


namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarCOE
{
    public class AgregarCOEHandler : IRequestHandler<AgregarCOEComando, bool>
    {
        private readonly IActuariaRepository _repository;
        private readonly ISesionService _sesionService;
        private readonly IGenerarAcuse _generarAcuse;

        public AgregarCOEHandler(IMapper mapper, IActuariaRepository repository, ISesionService sesionService, ILogger logger, IGenerarAcuse generarAcuse)
        {
            _repository = repository;
            _sesionService = sesionService;
            _generarAcuse = generarAcuse;
        }

        public async Task<bool> Handle(AgregarCOEComando request, CancellationToken cancellationToken)
        {
            
            return await _repository.AgregarCOE(request.COE, _sesionService.SesionActual.EmpleadoId);
            //var generaAcuseCoe = _generarAcuse.GeneraAcuseCOE(generaRequestAcuseCoe(request));
        }

        private GeneraAcuseCOERequest generaRequestAcuseCoe(AgregarCOEComando request)
        {
            return new GeneraAcuseCOERequest
            {
                TipoAsunto = request.COE.TipoAsunto.ToString(),
                NumeroExpediente = request.COE.Expediente,
                FechaEnvio = request.COE.FechaEnvio.ToString("dd MMMM yyyy"),
                Folio = "FolioPrueba",
                Correspondencia = "CorrespondenciaPrueba",
                NombreOrgano = _sesionService.SesionActual.NombreOficial ?? ""
            };
        }

    }
}


