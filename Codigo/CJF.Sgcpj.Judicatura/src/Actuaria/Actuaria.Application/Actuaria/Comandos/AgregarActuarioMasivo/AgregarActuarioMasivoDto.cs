using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using Microsoft.Data.SqlClient.Server;
using System.Data;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarActuarioMasivo;
public class AgregarActuarioMasivoDto : IMapFrom<AgregarActuarioMasivoM>
{
    public long AsuntoNeunId { get; set; }
    public int SintesisOrden { get; set; }
    public long ActuarioId { get; set; }
    public List<ParteNotificacionDto> PartesNotificaciones { get; set; }
}

public class ParteNotificacionDto : IMapFrom<ParteNotificacion>
{
    public long ParteId { get; set; }
    public long NotElecId { get; set; }
    public long TipoNotificacionID { get; set; }
    public bool TieneCOE { get; set; }

    private static readonly SqlMetaData[] recordSchema = {
        new SqlMetaData(nameof(ParteId), SqlDbType.BigInt),
        new SqlMetaData(nameof(NotElecId), SqlDbType.BigInt),
                 new SqlMetaData(nameof(TipoNotificacionID), SqlDbType.BigInt),
                 new SqlMetaData(nameof(TieneCOE), SqlDbType.Bit),
    };
    public SqlDataRecord toSqlDataRecord()
    {
        var record = new SqlDataRecord(recordSchema);
        record.SetSqlInt64(0, ParteId);
        record.SetSqlInt64(1, NotElecId);
        record.SetSqlInt64(2, TipoNotificacionID);
        record.SetSqlBoolean(3, TieneCOE);


        return record;
    }
}