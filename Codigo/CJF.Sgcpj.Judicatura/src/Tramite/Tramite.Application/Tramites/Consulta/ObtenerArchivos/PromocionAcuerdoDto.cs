using System.Data;
using Microsoft.Data.SqlClient.Server;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerArchivos;
public class PromocionAcuerdoDto
{
    public int? YearPromocion { get; set; }
    public int? NumeroOrden { get; set; }
    public int? EstadoPromocion { get; set; }
    public int? Proceso { get; set; }


    private static readonly SqlMetaData[] recordSchema = {
        new SqlMetaData(nameof(NumeroOrden), SqlDbType.Int),
                 new SqlMetaData(nameof(YearPromocion), SqlDbType.Int),
                 new SqlMetaData(nameof(EstadoPromocion), SqlDbType.Int),
                 new SqlMetaData(nameof(Proceso), SqlDbType.Int),
    };
    public SqlDataRecord toSqlDataRecord()
    {
        var record = new SqlDataRecord(recordSchema);
        if (NumeroOrden.HasValue)
            record.SetSqlInt32(0, NumeroOrden.Value);
        else
            record.SetDBNull(0);
        if (YearPromocion.HasValue)
            record.SetSqlInt32(1, YearPromocion.Value);
        else
            record.SetDBNull(1);
        if (EstadoPromocion.HasValue)
            record.SetSqlInt32(2, EstadoPromocion.Value);
        else
            record.SetDBNull(2);
        if (EstadoPromocion.HasValue)
            record.SetSqlInt32(3, Proceso.Value);
        else
            record.SetDBNull(3);
        return record;
    }
}

public class PromocionAcuerdoPersonasDto
{
    public long? PersonaId { get; set; } //considerada
    public long? PromoventeId { get; set; } //considerada
    public int? TipoNotificacionId { get; set; } //considerada
    public int? TipoConstanciaId { get; set; }
    public string? DescripcionConstancia { get; set; }
    public short? TipoPromovente { get; set; }
    public int? NumIntentosNotificacion { get; set; }
    public string? TextoOficioLibre { get; set; } //considerada
    public string? NombreParte { get; set; } //considerada
    public string? DescripcionPromovente { get; set; }
    public int? TipoAnexoId { get; set; } //considerada
    public bool TieneCOE { get; set; }

    private static readonly SqlMetaData[] recordSchema = {
        new SqlMetaData(nameof(PersonaId), SqlDbType.BigInt),
                 new SqlMetaData(nameof(PromoventeId), SqlDbType.BigInt), 
                 new SqlMetaData(nameof(TipoNotificacionId), SqlDbType.Int),
                 new SqlMetaData(nameof(TipoAnexoId), SqlDbType.Int),
                 new SqlMetaData(nameof(TextoOficioLibre), SqlDbType.NVarChar,1000),
                 new SqlMetaData(nameof(NombreParte), SqlDbType.NVarChar, 500),
                 new SqlMetaData(nameof(TieneCOE), SqlDbType.Bit),

    };
    public SqlDataRecord toSqlDataRecord()
    {
        var record = new SqlDataRecord(recordSchema);

        if (PersonaId.HasValue)
            record.SetSqlInt64(0, PersonaId.Value);
        else
            record.SetDBNull(0);

        if (PromoventeId.HasValue)
            record.SetSqlInt64(1, PromoventeId.Value);
        else
            record.SetDBNull(1);

        if (TipoNotificacionId.HasValue)
            record.SetSqlInt32(2, TipoNotificacionId.Value);
        else
            record.SetDBNull(2);

        if (TipoAnexoId.HasValue)
            record.SetSqlInt32(3, TipoAnexoId.Value);
        else
            record.SetDBNull(3);

        if (TextoOficioLibre != null)
            record.SetSqlString(4, TextoOficioLibre);
        else
            record.SetDBNull(4);

        if (NombreParte != null)
            record.SetSqlString(5, NombreParte);
        else
            record.SetDBNull(5);

        return record;
    }

}

public class PromocionAcuerdoAutoridadDto
{
    public int? TipoAnexoId { get; set; }
    public int? AnexoParteId { get; set; }
    public string? AnexoParteDescripcion { get; set; }
    public string? TextoOficioLibre { get; set; }
    public string? NombreAutoridad { get; set; }


    private static readonly SqlMetaData[] recordSchema = {
        new SqlMetaData(nameof(TipoAnexoId), SqlDbType.Int),
        new SqlMetaData(nameof(AnexoParteId), SqlDbType.Int),
        new SqlMetaData(nameof(AnexoParteDescripcion), SqlDbType.NVarChar,500),
        new SqlMetaData(nameof(TextoOficioLibre), SqlDbType.NVarChar, -1)

    };
    public SqlDataRecord toSqlDataRecord()
    {
        var record = new SqlDataRecord(recordSchema);

        if (TipoAnexoId.HasValue)
            record.SetSqlInt32(0, TipoAnexoId.Value);
        else
            record.SetDBNull(0);

        if (AnexoParteId.HasValue)
            record.SetSqlInt32(1, AnexoParteId.Value);
        else
            record.SetDBNull(1);

        if (AnexoParteDescripcion != null)
            record.SetSqlString(2, AnexoParteDescripcion);
        else
            record.SetDBNull(2);

        if (TextoOficioLibre != null)
            record.SetSqlString(3, TextoOficioLibre);
        else
            record.SetDBNull(3);

        return record;
    }


}