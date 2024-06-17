using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Seguridad.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Seguridad.Application.Seguridad.Comandos.RefrescarSesion;
[SesionNoRequiredAttribute]
public class RefrescarSesionComando : IRequest<bool>
{
    public int OrganismoId { get; set; }
    public string RefreshToken { get; set; }
}

public class RefrescarSesionComandoHandler : IRequestHandler<RefrescarSesionComando, bool>
{
    private readonly IMapper _mapper;
    private readonly ISeguridadRepository _seguridadRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IApplicationDbContext _applicationDbContext;

    public RefrescarSesionComandoHandler(IMapper mapper, ISeguridadRepository seguridadRepository, ICurrentUserService currentUserService)
    {
        _mapper = mapper;
        _seguridadRepository = seguridadRepository;
        _currentUserService = currentUserService;
    }
    public async Task<bool> Handle(RefrescarSesionComando request, CancellationToken cancellationToken)
    {
        bool resultado = false;
        var idEmpleado = Convert.ToInt32(_currentUserService.EmpleadoId);
        var nonce = _currentUserService.Nonce.ToString();

        await _seguridadRepository.RefrescarSesion(idEmpleado, nonce, request.RefreshToken);
        resultado = true;
        return resultado;
    }
}