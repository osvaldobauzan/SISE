using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Seguridad.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Seguridad.Application.Seguridad.Consultas.ObtieneOrganismosConsulta;
public class OrganismoDto : IMapFrom<OrganismoPorUsuario>
{
    public int CircuitoOrden { get; set; }
    public int CatOrganismoId { get; set; }
    public string NombreOficial { get; set; }
    public int CatTipoOrganismoId { get; set; }
    public string Visible { get; set; }
    public int CatHorarioIngresoValidoId { get; set; }
    public string TurnoActivo { get; set; }
}
