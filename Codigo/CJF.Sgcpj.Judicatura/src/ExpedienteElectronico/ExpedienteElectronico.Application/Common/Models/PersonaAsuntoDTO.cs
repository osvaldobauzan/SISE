using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Utils;
using DocumentFormat.OpenXml.VariantTypes;

namespace ExpedienteElectronico.Application.Common.Models;
public class PersonaAsuntoDTO
{    
    public string Nombre { get; set; }
    public string APaterno { get; set; }
    public string AMaterno { get; set; }
    public int CatTipoPersonaId { get; set; }
    public int CatCaracterPersonaAsuntoId { get; set; }
    public int Sexo { get; set; }
    public int MayorEdad { get; set; }
    public Int16 CatTipoPersonaJuridicaId { get; set; }
    public string DenominacionDeAutoridad { get; set; }
    public Int16 ClasificaAutoridadGenericaId { get; set; }
    public int SujetoDerechoAgrario { get; set; }
    public int AceptaOponePublicarDatos { get; set; }    
    public int Foraneo { get; set; }
    public int Recurrente { get; set; }
    public int CaracterPromueveNombre { get; set; }
    public int VictimaOfendidoDelito { get; set; }
    public int ParteAdhesivaApelacion { get; set; }
    public string Alias { get; set; }
    public int EsParteGrupoVulnerable { get; set; }
    public int GrupoVulnerable { get; set; }
    public Int16 EdadMenor { get; set; }
    public Int16 HablaLengua { get; set; }
    public int Lengua { get; set; }
    public Int16 Traductor { get; set; }
    public string FechaAceptaOponePublicarDatos { get; set; }
    public DateTime? FechaAceptaOponePublicarDatosFecha { 
        get {
            try {
                return MappingUtils.ObtenerFechaDeCadena(FechaAceptaOponePublicarDatos);
            }
            catch
            {
                return null;
            }
        } 
    }

}
