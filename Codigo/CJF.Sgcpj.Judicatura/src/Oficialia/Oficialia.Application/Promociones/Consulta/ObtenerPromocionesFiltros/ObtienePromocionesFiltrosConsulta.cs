using CJF.Sgcpj.Judicatura.Oficialia.Application.Common.Models;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Consulta.ObtenerPromocionesFiltros;
public record class ObtienePromocionesFiltrosConsulta : IRequest<FiltroPromociones<FiltroSecretarioDto, FiltroOrigenDto, FiltroCapturoDto>>
{
    public int CatOrganismoId { get; set; }
}
