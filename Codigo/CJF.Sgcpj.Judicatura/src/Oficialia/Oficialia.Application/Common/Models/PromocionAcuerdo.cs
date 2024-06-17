using System.Data;
using Microsoft.Data.SqlClient.Server;

namespace CJF.Sgcpj.Judicatura.Application.Common.Models;
public class PromocionAcuerdo
{
    public int? YearPromocion { get; set; }
    public int? NumeroOrden { get; set; }
    public int? EstadoPromocion { get; set; }
  


    private static readonly SqlMetaData[] recordSchema = {
        new SqlMetaData(nameof(NumeroOrden), SqlDbType.Int),
                 new SqlMetaData(nameof(YearPromocion), SqlDbType.Int),                
                 new SqlMetaData(nameof(EstadoPromocion), SqlDbType.Int),
               
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
        return record;
    }
}

public class PromocionAcuerdoPersonas
{
    public long? PersonaId { get; set; }
    public long? PromoventeId { get; set; }
    public int? TipoNotificacionId { get; set; }
    public int? TipoConstanciaId { get; set; }
    public string? DescripcionConstancia { get; set; }
    public short? TipoPromovente { get; set; }
    public int? NumIntentosNotificacion { get; set; }

    private static readonly SqlMetaData[] recordSchema = {
        new SqlMetaData(nameof(PersonaId), SqlDbType.BigInt),
                 new SqlMetaData(nameof(PromoventeId), SqlDbType.BigInt),
                 new SqlMetaData(nameof(TipoNotificacionId), SqlDbType.Int),
                 new SqlMetaData(nameof(TipoConstanciaId), SqlDbType.Int),
                 new SqlMetaData(nameof(DescripcionConstancia), SqlDbType.NVarChar,500),
                 new SqlMetaData(nameof(TipoPromovente), SqlDbType.SmallInt),
                 new SqlMetaData(nameof(NumIntentosNotificacion), SqlDbType.Int),

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

        if (TipoConstanciaId.HasValue)
            record.SetSqlInt32(3, TipoConstanciaId.Value);
        else
            record.SetDBNull(3);

        if (DescripcionConstancia != null)
            record.SetSqlString(4, DescripcionConstancia);
        else
            record.SetDBNull(4);

        if (TipoPromovente.HasValue)
            record.SetSqlInt16(5, TipoPromovente.Value);
        else
            record.SetDBNull(5);

        if (NumIntentosNotificacion.HasValue)
            record.SetSqlInt32(6, NumIntentosNotificacion.Value);
        else
            record.SetDBNull(6);

        return record;
    }

}

public class PromocionAcuerdoAutoridad
{
    public int? TipoAnexoId { get; set; }
    public int? AnexoParteId { get; set; }
    public string? AnexoParteDescripcion { get; set; }

    private static readonly SqlMetaData[] recordSchema = {
        new SqlMetaData(nameof(TipoAnexoId), SqlDbType.Int),
        new SqlMetaData(nameof(AnexoParteId), SqlDbType.Int),                
        new SqlMetaData(nameof(AnexoParteDescripcion), SqlDbType.NVarChar,500),             

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

        return record;
    }


}