namespace CJF.Sgcpj.Judicatura.Application.Common.Models;
public class AgregarPromocion
{
    public int CatOrganismoId { get; set; }
    public CatalogoAsunto TipoAsunto { get; set; }
    public string NumeroOCC { get; set; }
    public string NumeroExpediente { get; set; }
    public long EmpleadoId { get; set; }
    public CatalogoProcedimiento TipoProcedimiento { get; set; }
    public long AsuntoNeunId { get; set; }
    public CatalogoCuaderno Cuaderno { get; set; }
    public DateTime FechaPresentacion { get; set; }
    public string HoraPresentacion { get; set; }
    public int? ClasePromocion { get; set; }
    public CatalogoTipoPromovente? TipoPromoventeCat { get; set; }
    public string? TipoPromovente { get; set; }
    public int ClasePromovente
    {
        get => TipoPromovente switch
        {
            "parte" => 1,
            "promovente" => 2,
            "autoridad" => 3
        };
    }
    public CatalogoContenido Contenido { get; set; }
    public int Copias { get; set; }
    //public int? Anexos { get; set; }
    public int SecretarioId { get; set; }
    public string Observaciones { get; set; }
    public string IpUsuario { get; set; }
    public int? Origen { get; set; }//OrigenPromocion
    public int Registro { get; set; }
    public int Orden { get; set; }

    //TODO:Parametros para PromocionArchivo
    public int YearPromocion { get; set; }
    public int Clase { get; set; }
    public int Descripcion { get; set; }
    public int Caracter { get; set; }
    public string NombreArchivo { get; set; }
    public int NumeroConsecutivo { get; set; }
    public int OrigenArchivo { get; set; }//Origen
    public int Fojas { get; set; }
 
}

public class DatosPromocion
{
    public int NumeroOrden { get; set; }
    public long AsuntoNeunId { get; set; }
    public string? NombreArchivo { get; set; }
    public int NumeroConsecutivo { get; set; }
}
public class AsuntoPromocion
{
    public long AsuntoNeunId { get; set; }
}