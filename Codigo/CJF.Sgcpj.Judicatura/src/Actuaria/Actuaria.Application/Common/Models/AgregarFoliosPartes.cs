using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient.Server;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
public class AgregarFoliosPartes
{
    public long AsuntoNeunId { get; set; }
    public long  AsuntoDocumentoId { get; set; }
    public List<ParteNotificacionFolios> PartesNotificaciones { get; set; }
}

public class ParteNotificacionFolios
{
    public long ParteId { get; set; }
    public long PromoventeId { get; set; }
    public int TipoNotificacionId { get; set; }
    public int TipoAnexoId { get; set; }
    public string TextoOficioLibre { get; set; }
    public string NombreParte { get; set; }

    private static readonly SqlMetaData[] recordSchema = {
        new SqlMetaData(nameof(ParteId), SqlDbType.BigInt),
        new SqlMetaData(nameof(PromoventeId), SqlDbType.BigInt),
        new SqlMetaData(nameof(TipoNotificacionId), SqlDbType.Int),
        new SqlMetaData(nameof(TipoAnexoId), SqlDbType.Int),
        new SqlMetaData(nameof(TextoOficioLibre), SqlDbType.NVarChar,500),
        new SqlMetaData(nameof(NombreParte), SqlDbType.VarChar,100),
    };
    public SqlDataRecord toSqlDataRecord()
    {
        var record = new SqlDataRecord(recordSchema);
        record.SetSqlInt64(0, ParteId);
        record.SetSqlInt64(1, PromoventeId);
        record.SetSqlInt32(2, TipoNotificacionId);
        record.SetSqlInt32(3, TipoAnexoId);
        record.SetSqlString(4, TextoOficioLibre);
        record.SetSqlString(5, NombreParte);

        return record;
    }
}