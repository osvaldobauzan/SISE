using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Sentencias.Domain.Entities;
using static CJF.Sgcpj.Judicatura.Sentencias.Domain.Entities.Sentencia;

namespace CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Comandos.SubirSentenciaComando;

public class SentenciaDto : IMapFrom<Sentencia>
{
    //Datos registro único de sentencia
    //public long AsuntoDocumentoId { get; set; }

    //public int NumeroOrden { get; set; }

    //public int SintesisOrden { get; set; }

    public long AsuntoNeunId { get; set; }

    public int AsuntoId { get; set; }

    //Datos Archivo
    public string NomArchivoReal { get; set; }

    //public string NombreArchivo { get; set; }

    //public string ExtensionDocumento { get; set; }

    //public int IdRutaChunk { get; set; }

    //public short EstatusArchivo { get; set; }

    //public string TamanioArchivo { get; set; }

    public byte[] ArchivoBytes { get; set; }

    //Otros datos
    public int TipoArchivo { get; set; }

    public int Contenido { get; set; }

    public int TipoCuadernoId { get; set; }

    public DateTime FechaAuto { get; set; }

    public bool Sigilo { get; set; }

    public bool SentenciaDefinitiva { get; set; }

    public bool EsJDA { get; set; }

    public long TitularId { get; set; }

    public long SecretarioPId { get; set; }

    public long SecretarioCId { get; set; }

    public long ActuarioId { get; set; }

    public string Resumen { get; set; }

    //public int CatOrganismoId { get; set; }

    //public string IPUsuario { get; set; }

    //public int UsuarioCaptura { get; set; }

    //public int IdOrigen { get; set; }

    //public int TipoOrigen { get; set; }

    public int VersionPublica { get; set; }

    public int InfoReservada { get; set; }

    public int Perspectiva { get; set; }

    public int Criterio { get; set; }

    public int Trascedental { get; set; }

    public short EsTratadoInternacional { get; set; }

    public int TipoActo { get; set; }

    public int NombreTratado { get; set; }

    public int Derechos { get; set; }

    public int DerechoEspecifico { get; set; }

    public string TipoActoOtro { get; set; }

    public int SolicitudReparacion { get; set; }

    public int SolicitudReparacionOpcion { get; set; }

    public string SolicitudReparacionOtro { get; set; }

    public int? LecturaFacil { get; set; }

    public int TemaEquidadGenero { get; set; }

    public int? AplicacionEfectivaDerechoMujeres { get; set; }

    public int TemaAsuntosInternacionales { get; set; }

    public int AplicaCriteriosPerspectivaGenero { get; set; }

    public string CriterioPerspectivaGeneroAplicado { get; set; }

    public string Justificacion { get; set; }

    public List<DeterminacionPromocion> PromocionesDeterminaciones { get; set; }
}
