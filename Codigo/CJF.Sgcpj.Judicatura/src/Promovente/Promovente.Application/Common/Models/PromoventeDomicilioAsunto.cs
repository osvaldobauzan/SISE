using Microsoft.Data.SqlClient.Server;
using System.Data;

namespace CJF.Sgcpj.Judicatura.Promovente.Application.Common.Models;

public class PromoventeDomicilioAsunto
{
    public int DomicilioPromoventeId { get; set; }
    public int PromoventeId { get; set; }
    public string? CalleNumero { get; set; }
    public string? Colonia { get; set; }
    public int? CodigoPostal { get; set; }
    public int? Entidad { get; set; }
    public int? Delegacion { get; set;}
    public string? Localidad { get; set; }
    public int? RegistroEmpleadoId { get; set; }
    public DateTime? FechaCaptura { get; set; }

    private static readonly SqlMetaData[] recordSchema = {
       
                new SqlMetaData(nameof(CalleNumero), SqlDbType.VarChar,100),
                new SqlMetaData(nameof(Colonia), SqlDbType.VarChar, 50),
                new SqlMetaData(nameof(CodigoPostal), SqlDbType.Int),
                new SqlMetaData(nameof(Entidad), SqlDbType.Int),
                new SqlMetaData(nameof(Delegacion), SqlDbType.Int),
                new SqlMetaData(nameof(Localidad), SqlDbType.VarChar, 100),
                new SqlMetaData(nameof(RegistroEmpleadoId), SqlDbType.Int)
    };
    public SqlDataRecord toSqlDataRecord()
    {
        var record = new SqlDataRecord(recordSchema);
        record.SetSqlString(0, CalleNumero);
        record.SetSqlString(1, Colonia);

        if (CodigoPostal.HasValue)
            record.SetSqlInt32(2, CodigoPostal.Value);
        else
            record.SetDBNull(2);
        if (Entidad.HasValue)
            record.SetSqlInt32(3, Entidad.Value);
        else
            record.SetDBNull(3);
        if (Delegacion.HasValue)
            record.SetSqlInt32(4, Delegacion.Value);
        else
            record.SetDBNull(4);
        record.SetString(5, Localidad);
        if (RegistroEmpleadoId.HasValue)
            record.SetSqlInt32(6, RegistroEmpleadoId.Value);
        else
            record.SetDBNull(6);
        
        return record;
    }
}