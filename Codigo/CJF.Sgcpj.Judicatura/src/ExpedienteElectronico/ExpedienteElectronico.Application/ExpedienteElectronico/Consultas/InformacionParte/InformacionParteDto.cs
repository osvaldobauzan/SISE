using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using ExpedienteElectronico.Application.Common.Models;

namespace ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.InformacionParte;
public class InformacionParteDto : IMapFrom<InformacionParteM>
{
    public int TipoAsuntoId { get; set; }
    public string Valor { get; set; }
    public int NoBloque { get; set; }
    public int PersonaId { get; set; }
    public int CampoDatosGenerales { get; set; }
    public string Descripcion { get; set; }
    public int Padre { get; set; }
    public string PadreDescripcion { get; set; }
    public int Orden { get; set; }
    public int PadreOrden { get; set; }
    public string NombreParte { get; set; }
}
