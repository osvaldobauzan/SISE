using System;
using System.Data;
using Microsoft.Data.SqlClient.Server;

namespace CJF.Sgcpj.Judicatura.Application.Common.Models;

public class PersonaAsunto
{
    public string Nombre { get; set; }
    public string? APaterno { get; set; }
    public string? AMaterno { get; set; }
    public Int16? CatTipoPersonaId { get; set; }
    public Int16 CatCaracterPersonaAsuntoId { get; set; }
    public Int16? CatTipoPersonaJuridicaId { get; set; }
    public int AceptaOponePublicarDatos { get; set; }
    public long? UsuarioCaptura { get; set; }
    public int? CaracterPromueveNombre { get; set; }
    public int? Denominacion { get; set; }
 
    private static readonly SqlMetaData[] recordSchema = {
        new SqlMetaData(nameof(Nombre), SqlDbType.VarChar, 500),
                 new SqlMetaData(nameof(APaterno), SqlDbType.VarChar, 50),
                new SqlMetaData(nameof(AMaterno), SqlDbType.VarChar, 50),
                new SqlMetaData(nameof(CatTipoPersonaId), SqlDbType.SmallInt),
                new SqlMetaData(nameof(CatCaracterPersonaAsuntoId), SqlDbType.SmallInt),
                new SqlMetaData(nameof(CatTipoPersonaJuridicaId), SqlDbType.SmallInt),
                new SqlMetaData(nameof(CaracterPromueveNombre), SqlDbType.Int)
    };
    public SqlDataRecord toSqlDataRecord() { 
        var record = new SqlDataRecord(recordSchema);
        record.SetString(0, Nombre);
        record.SetString(1, APaterno);
        record.SetString(2, AMaterno);
        if (CatTipoPersonaId.HasValue)
            record.SetSqlInt16(3, CatTipoPersonaId.Value);
        else
            record.SetDBNull(3);
        record.SetSqlInt16(4, CatCaracterPersonaAsuntoId);
        if (CatTipoPersonaJuridicaId.HasValue)
            record.SetSqlInt16(5, CatTipoPersonaJuridicaId.Value);
        else
            record.SetDBNull(5);
        if (CaracterPromueveNombre.HasValue)
            record.SetSqlInt32(6, CaracterPromueveNombre.Value);
        else
            record.SetDBNull(6);
        return record;
    }
}