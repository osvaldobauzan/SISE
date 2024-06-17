using CJF.Sgcpj.Judicatura.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Oficialia.Domain.Entities;

namespace CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Consulta.ObtenerDetalleCargaMasiva;
public class DetalleAlertaDto : IMapFrom<DetalleAlerta>
{
    public int NumeroRegistro { get; set; }
    public string NumeroExpediente { get; set; }
    public string TipoAsunto { get; set; }
    public string TipoProcedimiento { get; set; }
    public string Mesa { get; set; }
    public int SecretarioId { get; set; }
}
