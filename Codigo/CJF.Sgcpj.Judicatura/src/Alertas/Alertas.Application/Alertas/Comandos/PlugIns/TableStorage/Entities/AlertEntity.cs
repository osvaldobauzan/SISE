using Azure;
using Azure.Data.Tables;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Alertas.Application.Alertas.Comandos.PlugIns.TableStorage.Entities;
internal class AlertEntity : ITableEntity
{
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
    public DateTime CreatedOn { get; set; }

    public AlertType TipoDeAlerta { get; set; }
    public string OrganismoId { get; set; }

    public string Emisor { get; set; }
    public string Mensaje { get; set; }
    public string Parte { get; set; }
    public string Receptor { get; set; }
    public string Estado { get; set; }
}
