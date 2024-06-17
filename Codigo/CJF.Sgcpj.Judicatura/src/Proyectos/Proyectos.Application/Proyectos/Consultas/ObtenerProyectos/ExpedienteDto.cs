using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Proyectos.Domain.Entities;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerProyectos;

public class ExpedienteDto : IMapFrom<Expediente>
{
    public int AsuntoNeunId { get; set; }

    public string AsuntoAlias { get; set; }

    public int CatTipoOrganismoId { get; set; }

    public int CatOrganismoId { get; set; }

    public string CatTipoAsunto { get; set; }

    public int CatTipoAsuntoId { get; set; }

    public string TipoProcedimiento { get; set; }

    public string NombreCorto { get; set; }

    public int TipoCuadernoId { get; set; }

    public string Cuaderno { get; set; }
}
