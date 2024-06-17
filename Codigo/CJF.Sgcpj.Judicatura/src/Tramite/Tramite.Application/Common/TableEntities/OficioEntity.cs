using Azure;
using Azure.Data.Tables;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Common.TableEntities;
internal class OficioEntity : ITableEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public string AcuerdoId { get; set; }
    public string Status { get; set; }
    public string MensajeError { get; set; }
    public string OficioActual { get; set; }
    public int Reintento { get; set; }
    public string MensajeSerializado { get; set; }
    public DateTime CreatedOn { get; set; }

    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}
