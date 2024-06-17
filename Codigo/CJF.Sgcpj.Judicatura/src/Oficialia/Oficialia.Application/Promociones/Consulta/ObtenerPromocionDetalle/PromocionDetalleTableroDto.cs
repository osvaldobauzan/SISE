﻿using CJF.Sgcpj.Judicatura.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerPromocionDetalle;

public class PromocionDetalleTableroDto : IMapFrom<PromocionDetalleTablero>
{
    public long? No { get; set; }
    public long? AsuntoNeunId { get; set; }
    public string? Expediente { get; set; }
    public string? CatTipoAsunto { get; set; }
    public int? CatTipoAsuntoId { get; set; }
    public string? TipoProcedimiento { get; set; }
    public int? TipoProcedimientoId { get; set; }
    public string? Cuaderno { get; set; }
    public int? CuadernoId { get; set; }
    public int? NumeroRegistro { get; set; }
    public string? OrigenPromocion { get; set; }
    public string? SecretarioNombre { get; set; }
    public int? SecretarioId { get; set; }
    public string? UserName { get; set; }
    public string? Mesa { get; set; }
    public DateTime? FechaPresentacion { get; set; }
    public string? HoraPresentacion { get; set; }
    public string? TipoPromociones { get; set; }
    public string? Contenido { get; set; }
    public int? ContenidoId { get; set; }
    public string? PromoventeNombre { get; set; }
    public string? PromoventeApellidoPaterno { get; set; }
    public string? PromoventeApellidoMaterno { get; set; }
    public int IdPromovente { get; set; }
    public string? DenominacionAutoridad { get; set; }
    public string? Promovente { get; set; }
    public string? ClasePromoventeDescripcion { get; set; }
    public int? NumeroCopias { get; set; }
    public short? Fojas { get; set; }
    public int? NumeroAnexos { get; set; }
    public int? Registrada { get; set; }
    public int? ConArchivo { get; set; }
    public int? EsDemanda { get; set; }
    public int? OrigenPromocionId { get; set; }
    public string? Folio { get; set; }
    public int? EsPromocionE { get; set; }
    public short? CatAutorizacionDocumentosId { get; set; }
    public string? NombreArchivo { get; set; }
    public int? Origen { get; set; }
    public int? NumeroOrden { get; set; }
    public string? TipoPromovente { get; set; }
    public string? ParteAsociadaPromovente { get; set; }
    public string? CaracterParteAsociadaPromovente { get; set; }
    public string? TipoPersonaParteAsociadaPromovente { get; set; }
    public string? CaracterParte { get; set; }
    public string? TipoPersonaParte { get; set; }
    public string? Capturo { get; set; }
    public DateTime? FechaCaptura { get; set; }
    public string? NombreArchivoPromocion { get; set; }
    public string? Anexos { get; set; }
    public string? Archivos { get; set; }
    public string? OCC { get; set; }
    public string? BoletaOCC { get; set; }
    public string? RutaCompletaBoleta { get; set; }
    public string? NombreOficial { get; set; }

    public int? TotalArchivos { get; set; }
    public string? Tipo { get; set; }
    public int? ProcedimientoId { get; set; }
    public int? TipoPromoventeId { get; set; }
    public short? TipoParteAsociadaPromoventeId { get; set; }
    public short? CaracterParteAsociadaId { get; set; }
    public string? ParteAsociadaNombre { get; set; }
    public string? ParteAsociadaApellidoPaterno { get; set; }
    public string? ParteAsociadaApellidoMaterno { get; set; }
    public long? ParteAsociadaId { get; set; }
    public short? CaracterParteId { get; set; }
    public short? TipoPersonaParteId { get; set; }
    public string? ParteNombre { get; set; }
    public string? ParteApellidoPaterno { get; set; }
    public string? ParteApellidoMaterno { get; set; }
    public int? AutoridadJudicialId { get; set; }
    public int? AutoridadOrganismoId { get; set; }
    public int? CatOrganismoId { get; set; }
    public string? CatOrganismo { get; set; }
    public int? YearPromocion { get; set; }

    public DateTime? FechaAlta { get; set; }
    public string? PromoventeRegistro { get; set; }
}
