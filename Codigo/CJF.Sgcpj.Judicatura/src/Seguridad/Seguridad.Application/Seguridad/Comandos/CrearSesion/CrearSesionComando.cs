using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Seguridad.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Seguridad.Application.Seguridad.Comandos.CrearSesion;
[SesionNoRequiredAttribute]
public class CrearSesionComando : IRequest<SesionDto>
{
    public int EmpleadoId { get; set; }
    public int OrganismoId { get; set; }
    public string RefreshToken { get; set; }

}

public class CrearSesionComandoHandler : IRequestHandler<CrearSesionComando, SesionDto>
{
    private readonly IMapper _mapper;
    private readonly ISeguridadRepository _seguridadRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IApplicationDbContext _applicationDbContext;

    public CrearSesionComandoHandler(IMapper mapper, ISeguridadRepository seguridadRepository, ICurrentUserService currentUserService)
    {
        _mapper = mapper;
        _seguridadRepository = seguridadRepository;
        _currentUserService = currentUserService;
    }
    public async Task<SesionDto> Handle(CrearSesionComando request, CancellationToken cancellationToken)
    {
        SesionDto sesionDto = null;
        var idEmpleado = Convert.ToInt32(_currentUserService.EmpleadoId);
        var idOrganismoId = Convert.ToInt32(request.OrganismoId);
        var sesion = await _seguridadRepository.ObtenerDatosSesion(idEmpleado, idOrganismoId);
        sesion.Nonce = _currentUserService.Nonce.ToString();
        sesion.RefrehToken = request.RefreshToken;

        sesionDto = _mapper.Map<SesionDto>(sesion);
        if (await _seguridadRepository.IniciarSesion(sesion))
        {
            return sesionDto;
        }

        return sesionDto;
    }
}