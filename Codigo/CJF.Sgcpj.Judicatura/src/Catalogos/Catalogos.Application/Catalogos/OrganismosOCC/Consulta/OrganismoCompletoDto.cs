using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.OrganismosOCC.Consulta;
public class OrganismoCompletoDto : IMapFrom<CatalogoOrganismosOCC>
{
    public int CircuitoOrden { get; set; }
    public int CatOrganismoId { get; set; }
    public string NombreOficial { get; set; }
    public int CatTipoOrganismoId { get; set; }

}
