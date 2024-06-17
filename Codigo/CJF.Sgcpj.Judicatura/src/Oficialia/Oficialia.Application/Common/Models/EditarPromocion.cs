using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Application.Catalogos.Contenido.Consulta;

namespace CJF.Sgcpj.Judicatura.Application.Common.Models;
public  class EditarPromocion
{
    public long AsuntoNeunId { get; set; }
    public CatalogoAsunto TipoAsunto { get; set; }
    public int IdOrganismo { get; set; }
    public int NumeroOrden { get; set; }
    public int NumeroRegistro { get; set; }
    public DateTime FechaPresentacion { get; set; }
    public string HoraPresentacion { get; set; }
    public int ClasePromocion { get; set; }
    public int Partes { get; set; }
    public int IdTipoPromovente { get; set; }
    public CatalogoContenido Contenido { get; set; }
    public int Copias { get; set; }
    public int Anexos { get; set; }
    public DateTime FechaEntrega { get; set; }
    public int PersonaRecibe { get; set; }
    public int SecretarioId { get; set; }
    public int RegistroEmpleadoId { get; set; }
    public int YearPromocion { get; set; }
    public int EstadoPromocion { get; set; }
    public int Origen { get; set; }
    public int NumeroAnexos { get; set; }
    public string Fojas { get; set; }
    public string Observaciones { get; set; }
    public bool SelectorRegistro { get; set; }
    public CatalogoCuaderno Cuaderno { get; set; }
    public string IpUsuario { get; set; }
    public string? Mesa { get; set; }
    public CatalogoTipoPromovente? TipoPromoventeCat { get; set; }
    public string? TipoPromovente { get; set; }
    public int PersonaId { get; set; }
    public int ClasePromovente
    {
        get => TipoPromovente switch
        {
            "parte" => 1,
            "promovente" => 2,
            "autoridad" => 3
        };
    }

    public string NumeroExpediente { get; set; }
    public string NumeroOCC { get; set; }   
    public long? AsuntoNeunIdNuevo { get; set; }
    public CatalogoProcedimiento TipoProcedimiento { get; set; }
    public bool EsPromoventeExistente { get; set; }
    public CatalogoPromoventeExistente PromoventeExistente { get; set; }
    public CatalogoParteExistente PromoventeAutoridadExistente { get; set; }
    public CatalogoParteAutoridad PromoventeAutoridad { get; set; }

}
