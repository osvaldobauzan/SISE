using Azure;
using Azure.Data.Tables;

namespace CJF.Sgcpj.Judicatura.Tramite.Domain.Entities;
public class DocumentoTableEntity : ITableEntity
{
    public string Nombre { get; set; }
    public string TipoArchivo { get; set; }

    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}
