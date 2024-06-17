using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Seguridad.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Seguridad.Application.Seguridad.Comandos.CerrarSesion;
public class CerrarSesionComando : IRequest<bool>
{
    public int EmpleadoId { get; set; }
    public int OrganismoId { get; set; }
    public string RefreshToken { get; set; }


}


public class BorrarSesionComandoHandler : IRequestHandler<CerrarSesionComando, bool>
{
    private readonly IMapper _mapper;
    private readonly ISeguridadRepository _seguridadRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IApplicationDbContext _applicationDbContext;

    public BorrarSesionComandoHandler(IMapper mapper, ISeguridadRepository seguridadRepository, ICurrentUserService currentUserService)
    {
        _mapper = mapper;
        _seguridadRepository = seguridadRepository;
        _currentUserService = currentUserService;
    }
    public async Task<bool> Handle(CerrarSesionComando request, CancellationToken cancellationToken)
    {
        var idEmpleado = Convert.ToInt32(_currentUserService.EmpleadoId);
        return await _seguridadRepository.CerrarSesion(idEmpleado, request.RefreshToken);
    }
}
