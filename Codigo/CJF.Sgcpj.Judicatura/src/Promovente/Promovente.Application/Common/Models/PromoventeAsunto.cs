using System.Data;
using Microsoft.Data.SqlClient.Server;

namespace CJF.Sgcpj.Judicatura.Promovente.Application.Common.Models;

public class PromoventeAsunto
{
    public int? PersonaId { get; set; }
    public int? Tipo { get; set; }
    public string? Nombre { get; set; }
    public string? APaterno { get; set; }
    public string? AMaterno { get; set; }

    private static readonly SqlMetaData[] recordSchema = {
        new SqlMetaData(nameof(PersonaId), SqlDbType.Int),
                 new SqlMetaData(nameof(Tipo), SqlDbType.Int),
                new SqlMetaData(nameof(Nombre), SqlDbType.VarChar, 50),
                new SqlMetaData(nameof(APaterno), SqlDbType.VarChar, 50),
                new SqlMetaData(nameof(AMaterno), SqlDbType.VarChar, 50)
    };
    public SqlDataRecord toSqlDataRecord()
    {
        var record = new SqlDataRecord(recordSchema);
        if (PersonaId.HasValue)
            record.SetSqlInt32(0, PersonaId.Value);
        else
            record.SetDBNull(0);
        if (Tipo.HasValue)
            record.SetSqlInt32(1, Tipo.Value);
        else
            record.SetDBNull(1);
        record.SetString(2, Nombre);
        record.SetString(3, APaterno);
        record.SetString(4, AMaterno);
        
        return record;
    }
}