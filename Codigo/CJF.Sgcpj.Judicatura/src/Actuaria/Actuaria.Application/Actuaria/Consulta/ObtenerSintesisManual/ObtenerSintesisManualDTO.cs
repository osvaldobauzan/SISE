using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerSintesisManual;
public class ObtenerSintesisManualDTO 
{
     public long AsuntoNeunId { get; set; }
     public string  AsuntoAlias {get; set;}
     public long CatTipoOrganismoId {get; set;}
     public string  CatTipoOrganismo {get; set;}
     public long CatOrganismoId {get; set;}
     public string  CatOrganismo {get; set;}
     public long CatTipoAsuntoId {get; set;}
     public string  CatTipoAsunto {get; set;}
     public int  CatTipoProcedimiento {get; set;}
     public string  TipoProcedimiento {get; set;}
     public long TipoProcedimientoId {get; set;}
     public long AsuntoId {get; set;}
     public int  SintesisOrden {get; set;}
     public DateTime  FechaAuto {get; set;}
     public string Sintesis {get; set;}
     public DateTime  FechaPublicacion {get; set;}
     public long  Titular {get; set;}
     public long  Actuario {get; set;}
     public long  ClasificacionCuaderno {get; set;}
     public long  UsuarioCaptura {get; set;}
     public DateTime  FechaAlta {get; set;}
     public long  Parte1 {get; set;}
     public long  Parte2 {get; set;}
     public bool  Parte1YOtros {get; set;}
     public bool  Parte2YOtros {get; set;}
     public long  TipoCuaderno {get; set;}

    public PersonasAsunto Quejoso { get; set;}
    public PersonasAsunto Autoridad { get; set;}

}
