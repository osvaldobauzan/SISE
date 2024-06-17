using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.LibretaOficios.Application.Common.Models;
using CJF.Sgcpj.Judicatura.LibretaOficios.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.LibretaOficios.Application.LibretaOficios.Comandos.InsertarOficio;
public class InsertarOficioHandler : IRequestHandler<InsertarOficioFiltro, bool>

{
    private readonly ILibretaOficiosRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;


    public InsertarOficioHandler(ILibretaOficiosRepository repository, IMapper mapper, ISesionService sesionService)
    {
        _repository = repository;
        _mapper = mapper;
        _sesionService = sesionService;
    }
    public async Task<bool> Handle(InsertarOficioFiltro request, CancellationToken cancellationToken)
    {
        var dato = await _repository.InsertarOficio(request);
        return dato;
    }
}
