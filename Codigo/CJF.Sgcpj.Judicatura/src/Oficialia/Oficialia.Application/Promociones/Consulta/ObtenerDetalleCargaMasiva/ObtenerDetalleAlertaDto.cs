using CJF.Sgcpj.Judicatura.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Consulta.ObtenerDetalleCargaMasiva;
public class ObtenerDetalleAlertaDto : IMapFrom<ObtenerDetalleAlerta>
{
    public int NumeroRegistro { get; set; }
    public string NumeroExpediente { get; set; }
    public string TipoAsunto { get; set; }
    public string TipoProcedimiento { get; set; }
    public string Mesa { get; set; }
    public int SecretarioId { get; set; }

}
