using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Promovente.Application.Common.Models;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Promovente.Application.Usuarios.ParteExistente.Consulta;

public class ObtieneCatalogoParteExistenteHandler : IRequestHandler<ObtieneCatalogoParteExistente, List<CatalogoParteExistenteDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;

    public ObtieneCatalogoParteExistenteHandler(IMapper mapper, IApplicationDbContext applicationDbContext)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
    }

    async Task<List<CatalogoParteExistenteDto>> IRequestHandler<ObtieneCatalogoParteExistente, List<CatalogoParteExistenteDto>>.Handle(ObtieneCatalogoParteExistente request, CancellationToken cancellationToken)
    {
        var listaCatalogosParteExistente = new List<CatalogoParteExistenteDto>();
        var listaParteExistenteParametros = new List<SqlParameter>();

        var asuntoNeunId = new SqlParameter("@pi_AsuntoNeunId", request.AsuntoNeunId);
        var pi_NoExp = new SqlParameter("@pi_NoExp", request.NoExp);
        var pi_Texto = new SqlParameter("@pi_Texto", request.Texto);
        var pi_Modulo = new SqlParameter("@pi_Modulo", request.Modulo);
        var pi_TipoParte = new SqlParameter("@pi_TipoParte", request.TipoParte);
        listaParteExistenteParametros.Add(asuntoNeunId);
        listaParteExistenteParametros.Add(pi_NoExp);
        listaParteExistenteParametros.Add(pi_Texto);
        listaParteExistenteParametros.Add(pi_Modulo);
        listaParteExistenteParametros.Add(pi_TipoParte);
        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<CatalogoParteExistente>("[SISE3].[pcPersonasXAsunto]", listaParteExistenteParametros.ToArray());
        listaCatalogosParteExistente = _mapper.Map<List<CatalogoParteExistenteDto>>(itemsCatalogo);

        return await Task.FromResult(listaCatalogosParteExistente);
    }
}
