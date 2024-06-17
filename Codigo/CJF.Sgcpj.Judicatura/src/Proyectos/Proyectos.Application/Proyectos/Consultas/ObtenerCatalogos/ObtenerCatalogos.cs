using MediatR;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerCatalogos;

public class ObtenerCatalogos : IRequest<ObtenerCatalogosDto>
{
    public int CatTipoAsuntoId { get; set; }
}
