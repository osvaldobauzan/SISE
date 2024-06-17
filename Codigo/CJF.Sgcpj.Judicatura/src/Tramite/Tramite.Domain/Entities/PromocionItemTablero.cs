namespace CJF.Sgcpj.Judicatura.Tramite.Domain.Entities;

public class PromocionItemTablero
{
    public int No { get; set; }
    public long AsuntoNeunId { get; set; }
    public string Expediente { get; set; }
    public string CatTipoAsunto { get; set; }
    public int CatTipoAsuntoId { get; set; }
    public string TipoProcedimiento { get; set; }
    public string Cuaderno { get; set; }
    public int NumeroRegistro { get; set; }
    public string OrigenPromocion { get; set; }
    public string NombreOrigen { get; set; }
    public string Secretario { get; set; }
    public string SecretarioUserName { get; set; }
    public int IdSecretario { get; set; }
    public string Mesa { get; set; }
    public DateTime FechaPresentacion { get; set; }
    public string TipoPromociones { get; set; }
    public string TipoContenido { get; set; }
    public string Promovente { get; set; }    
    public int IdPromovente { get; set; }
    public string ClasePromovente { get; set; }
    public int TOTAL { get; set; }
    public int SinCaptura { get; set; }
    public int Capturadas { get; set; }
    public int Asignadas { get; set; }
    public int CatOrganismoId { get; set; }
    public int NumeroCopias { get; set; }
    public int NumeroAnexos { get; set; }
    public int EstatusPromocion { get; set; }
    public int EstadoAcuerdo { get; set; }
    public int OrigenPromocionId { get; set; }
    public int Origen { get; set; }
    public long Fojas { get; set; }
    public int NumeroOrden { get; set; }
    public int YearPromocion { get; set; }
    public long? kIdElectronica { get; set; }
    public string UsuarioCaptura { get; set; }
    public DateTime? FechaCaptura { get; set; }    
    public int? CatAutorizacionDocumentosId { get; set; }
    public bool EsDemandaElectronica { get { return EsDemanda && EsPromocionE; } }
    public bool EsDemanda { get; set; } 
    public bool EsPromocionE { get; set; }
    public bool ConArchivo { get; set; }
    public string OrigenPromocionDescripcion { get; set; }


}
