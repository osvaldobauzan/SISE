using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ObtenerDetalleCaracter;
public class ObtenerDetalleCaracterDto
{
    public string DescripcionTipoPersona { get; set; }
    public string DenominacionDeAutoridad { get; set; }
    public string DescripcionCaracterPersona { get; set; }
    public string DescripcionClasificaAutoridadGenerica { get; set; }
    public float PersonaId { get; set; }
    public string Nombre { get; set; }
    public string APaterno { get; set; }
    public string AMaterno { get; set; }
    public float CatCaracterPersonaAsuntoId { get; set; }
    public int Foraneo { get; set; }
    public string Alias { get; set; }
    public int CatAutoridadId { get; set; }
    public float ClasificaAutoridadGenericaId { get; set; }
    public int EsParteGrupoVulnerable { get; set; }
    public int GrupoVulnerable { get; set; }
    public bool TieneCaptura { get; set; }
    public float CatTipoPersonaId { get; set; }
    public int Recurrente { get; set; }

}
