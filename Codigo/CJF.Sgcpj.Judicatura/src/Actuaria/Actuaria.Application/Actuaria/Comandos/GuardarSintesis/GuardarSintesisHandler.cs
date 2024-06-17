using AutoMapper;
using AutoMapper.Configuration;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Actuaria.Domain.Entities;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Alertas;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.GuardarSintesis;
public class GuardarSintesisHandler : IRequestHandler<GuardarSintesisComando, bool>
{
    private readonly IMapper _mapper;
    private readonly IActuariaRepository _actuariaRepository;

    public GuardarSintesisHandler(IMapper mapper, IActuariaRepository actuariaRepository)
    {
        _mapper = mapper;
        _actuariaRepository = actuariaRepository;
    }
    public async Task<bool> Handle(GuardarSintesisComando request, CancellationToken cancellationToken)
    {
        var sintesis = _mapper.Map<GuardarSintesisAcuerdo>(request.SintesisAcuerdo);

        var resultado = await _actuariaRepository.GuardarSintesisAcuerdo(sintesis);
        return resultado;
    }
}
