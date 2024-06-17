using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Anexo.Consulta;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.ContenidoTramite.Consulta;
public class ObtieneCatalogoContenidoTramiteHandler : IRequestHandler<ObtieneCatalogoContenidoTramite, List<ContenidoTramiteDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ISesionService _sesionService;

    public ObtieneCatalogoContenidoTramiteHandler(IMapper mapper, IApplicationDbContext applicationDbContext, ISesionService sesionService)
    {
        _mapper = mapper;
        _applicationDbContext = applicationDbContext;
        _sesionService = sesionService;
    }
    async Task<List<ContenidoTramiteDto>> IRequestHandler<ObtieneCatalogoContenidoTramite, List<ContenidoTramiteDto>>.Handle(ObtieneCatalogoContenidoTramite request, CancellationToken cancellationToken)
    {

        List<ContenidoTramiteDto> listaCatalogos = new List<ContenidoTramiteDto>();
        List<SqlParameter> contenidoTramiteParametros = new List<SqlParameter>();
        int catOrganismoId = 0;
        var piCatTipoCatalogoAsuntoId = new SqlParameter("@piCatTipoCatalogoAsuntoId", 496);
        var piCatOrganismoId = new SqlParameter("@piCatOrganismoId", SqlDbType.Int);
        piCatOrganismoId.Value = 0;

        var piCatTipoAsuntoId = new SqlParameter("@piCatTipoAsuntoId", SqlDbType.Int);
        piCatTipoAsuntoId.Value = 0;

        contenidoTramiteParametros.Add(piCatTipoCatalogoAsuntoId);
        contenidoTramiteParametros.Add(piCatOrganismoId);
        contenidoTramiteParametros.Add(piCatTipoAsuntoId);

        var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<CatalogoContenido>("usp_CatalogosSel", contenidoTramiteParametros.ToArray());
        itemsCatalogo.RemoveAll(r => r.DESCRIPCION.Contains("Audiencia "));
        listaCatalogos = _mapper.Map<List<ContenidoTramiteDto>>(itemsCatalogo);
       
        return await Task.FromResult(listaCatalogos);
    }
}
