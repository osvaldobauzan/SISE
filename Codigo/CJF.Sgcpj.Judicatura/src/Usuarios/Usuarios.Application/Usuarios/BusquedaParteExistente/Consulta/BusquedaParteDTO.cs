using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Usuarios.Application.Usuarios.BusquedaParteExistente.Consulta;
public class BusquedaParteDTO
{
    public int? CatTipoAsuntoId { get; set; }
    public string? NombreOficial { get; set; }
    public string? TipoAsuntoDescripcion { get; set; }
    public int? CatTipoOrganismoId { get; set; }
    public int? AsuntoId { get; set; }
    public long? AsuntoNeunId { get; set; }
    public string? AsuntoAlias { get; set; }
    public string? Nombre { get; set; }
    public string? APaterno { get; set; }
    public string? AMaterno { get; set; }
    public string? CatCaracterPersonaAsuntoDescripcion { get; set; }
}
