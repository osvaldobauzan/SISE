using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.EditarSintesis;
public class EditarSintesisHandler : IRequestHandler<EditarSintesisComando, bool>
{
    private readonly IMapper _mapper;
    private readonly IActuariaRepository _actuariaRepository;
    public EditarSintesisHandler(IMapper mapper, IActuariaRepository actuariaRepository)
    {
        _mapper = mapper;
        _actuariaRepository = actuariaRepository;
    }
    public async Task<bool> Handle(EditarSintesisComando request, CancellationToken cancellationToken)
    {
        var sintesis = _mapper.Map<EditarSintesisAcuerdo>(request.SintesisAcuerdo);

        var resultado = await _actuariaRepository.EditarSintesisAcuerdo(sintesis);
        return resultado;
    }
}