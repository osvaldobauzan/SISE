using System.Data;
using Microsoft.Data.SqlClient.Server;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Comandos.SubirProyectoConAudienciaComando;

public class MotivosParteDto
{
    public long? IdParte { get; set; }

    public int? IdMotivo { get; set; }

    public int? IdSentido { get; set; }

    public string? Descripcion { get; set; }

    private static readonly SqlMetaData[] RecordSchema =
    {
        new(nameof(IdParte), SqlDbType.BigInt),
        new(nameof(IdMotivo), SqlDbType.Int),
        new(nameof(IdSentido), SqlDbType.Int),
        new(nameof(Descripcion), SqlDbType.Text),
    };

    public SqlDataRecord ToSqlDataRecord()
    {
        var record = new SqlDataRecord(RecordSchema);

        if (IdParte.HasValue)
            record.SetInt64(0, IdParte.Value);
        else
            record.SetDBNull(0);

        if (IdMotivo.HasValue)
            record.SetSqlInt32(1, IdMotivo.Value);
        else
            record.SetDBNull(1);

        if (IdSentido.HasValue)
            record.SetSqlInt32(2, IdSentido.Value);
        else
            record.SetDBNull(2);

        if (!string.IsNullOrEmpty(Descripcion))
            record.SetSqlString(3, Descripcion);
        else
            record.SetDBNull(3);

        return record;
    }
}
