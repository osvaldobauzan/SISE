namespace CJF.Sgcpj.Judicatura.Seguridad.Application.Common.Models;


public class OrganismoPorUsuario
{
    public int CircuitoOrden { get; set; }
    public int CatOrganismoId { get; set; }
    public string NombreOficial { get; set; }
    public int CatTipoOrganismoId { get; set; }
    public string Visible { get; set; }
    public int CatHorarioIngresoValidoId { get; set; }
    public string TurnoActivo { get; set; }
}

