using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarSintesisManual;
public class AgregarSintesisManualHandler : IRequestHandler<AgregarSintesisManualRequest, bool>
{
    private readonly IActuariaRepository _actuariaRepository;
    private readonly ISesionService _sesionService;

    public AgregarSintesisManualHandler(IActuariaRepository actuariaRepository, ISesionService sesionService)
    {
        _actuariaRepository = actuariaRepository;
        _sesionService = sesionService;
    }

    public async Task<bool> Handle(AgregarSintesisManualRequest request, CancellationToken cancellationToken)
    {
        var parametros = new AgregarSintesisManualDto()
        {

            ClasificacionCuaderno = request.TipoCuaderno,
            TipoCuaderno = request.TipoCuaderno,
            AsuntoNeunId = request.AsuntoNeunId,
            FechaAuto = Convert.ToDateTime(request.FechaAuto),
            FechaPublicacion = Convert.ToDateTime(request.FechaPublicacion),
            Actuario = request.Actuario,
            Titular = request.Titular,
            Sintesis = request.Sintesis,
            Parte1 = request.Quejoso,
            Parte2 = request.Autoridad,
            CatOrganismoId = _sesionService.SesionActual.CatOrganismoId,
            UsuarioCaptura = _sesionService.SesionActual.EmpleadoId
        };
        return await _actuariaRepository.AgregarSintesisManual(parametros);
    }
}
