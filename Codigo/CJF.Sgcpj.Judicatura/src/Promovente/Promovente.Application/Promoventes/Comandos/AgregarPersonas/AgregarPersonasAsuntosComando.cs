using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Promovente.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Promovente.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Promovente.Application.Promoventes.Comandos.AgregarPersonas;
public class AgregarPersonasAsuntosComando : IRequest<long>
{
    public AgregarPersonasAsuntosDto PersonasAsuntos { get; set; }
}

public class AgregarPersonasAsuntosComandoHandler : IRequestHandler<AgregarPersonasAsuntosComando, long>
{
    private readonly IMapper _mapper;
    private readonly IPromoventesRepository _promoventesRepository;
    public AgregarPersonasAsuntosComandoHandler(IMapper mapper, IPromoventesRepository promoventesRepository)
    {
        _mapper = mapper;
        _promoventesRepository = promoventesRepository;
    }

    public async Task<long> Handle(AgregarPersonasAsuntosComando request, CancellationToken cancellationToken)
    {
        var personaAsunto = _mapper.Map<AgregarPersonaAsunto>(request.PersonasAsuntos);
        return await _promoventesRepository.AgregarPersona(personaAsunto);
    }
}