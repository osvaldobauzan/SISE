using AutoMapper;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Repositories;
using MediatR;

namespace ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.DatosGenerales;
public class DatosGeneralesConsultaHandler : IRequestHandler<DatosGeneralesConsulta, DatosGeneralesDto>
{
    private readonly IExpedienteElectronicoRepository _repository;
    private readonly IMapper _mapper;

    public DatosGeneralesConsultaHandler(IExpedienteElectronicoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<DatosGeneralesDto> Handle(DatosGeneralesConsulta request, CancellationToken cancellationToken)
    {
        var datos = await _repository.ObtenerDatosGenerales(request);
        var datosGenerales = _mapper.Map<DatosGeneralesDto>(datos);
        return datosGenerales;
    }
}
