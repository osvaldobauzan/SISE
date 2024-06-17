using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.OrganismosOCC.Consulta;
public class OrganosJurisdiccionalesDTO
{
    public int? CatOrganismoId { get; set; }
    public string? NombreOficial { get; set; }
    public short? CatCircuitoId { get; set; }
    public string? Circuito { get; set; }
    public int? CatTipoOrganismoId { get; set; }
    public string? CatTipoOrganismo { get; set; }
    public short? CatCircuitoClasificacionId { get; set; }
    public short? OrdenCircuito { get; set; }
    public short? OrdenRegion { get; set; }
    public short? CircuitoOrden { get; set; }

}
