using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerFiltrosTramite;
public record ObtieneFiltroTramiteConsulta : IRequest<FiltroTramite<ObtieneFiltroSecretarioDto,
    ObtieneFiltroOrigenDto, ObtieneFiltroTipoAsuntoDto, ObtieneFiltroCapturoDto, ObtieneFiltroPreautorizoDto,
    ObtieneFiltroAutorizoDto, ObtieneFiltroCanceloDto>>
{
    public int CatOrganismoId { get; set; }
}
