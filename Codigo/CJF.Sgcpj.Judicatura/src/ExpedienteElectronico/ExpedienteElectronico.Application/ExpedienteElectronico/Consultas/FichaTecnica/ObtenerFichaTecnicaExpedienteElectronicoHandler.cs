using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Models;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Repositories;

namespace ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.FichaTecnica;
public class ObtenerFichaTecnicaExpedienteElectronicoHandler : IRequestHandler<FichaTecnicaExpedienteElectronicoFiltro, List<FichaTecnicaExpedienteElectronico>>
{
    private readonly IExpedienteElectronicoRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    public ObtenerFichaTecnicaExpedienteElectronicoHandler(IExpedienteElectronicoRepository repository, IMapper mapper, ISesionService sesionService)
    {
        _repository = repository;
        _mapper = mapper;
        _sesionService = sesionService;
    }
    public async Task<List<FichaTecnicaExpedienteElectronico>> Handle(FichaTecnicaExpedienteElectronicoFiltro request, CancellationToken cancellationToken)
    {
        List<FichaTecnicaExpedienteElectronico> datos = await _repository.ObtenerFichaTecnicaExpediente(request);
        return datos;
    }
}
