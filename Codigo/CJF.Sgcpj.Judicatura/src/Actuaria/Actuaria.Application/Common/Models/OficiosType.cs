using System.Data;
using Microsoft.Data.SqlClient.Server;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
public class OficiosType
{
    public int? AnexoId { get; set; }
    public long? AsuntoNeunId { get; set; }
    public string Folio { get; set; }


    private static readonly SqlMetaData[] recordSchema = {
        new SqlMetaData(nameof(AnexoId), SqlDbType.Int),
         new SqlMetaData(nameof(AsuntoNeunId), SqlDbType.BigInt),
               
                 new SqlMetaData(nameof(Folio), SqlDbType.VarChar,50),
    };
    public SqlDataRecord toSqlDataRecord()
    {
        var record = new SqlDataRecord(recordSchema);
        if (AnexoId.HasValue)
            record.SetSqlInt32(0, AnexoId.Value);
        else
            record.SetDBNull(0);
        if (AsuntoNeunId.HasValue)
            record.SetSqlInt64(1, AsuntoNeunId.Value);
        else
            record.SetDBNull(1);
        if (!string.IsNullOrEmpty( Folio))
            record.SetString(2, Folio);
        else
            record.SetDBNull(2);

        return record;
    }
}
