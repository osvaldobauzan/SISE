using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Comandos.EstadoAcuerdoComandoRE;
public class EstadoAcuerdoREDto : IMapFrom<EstadoAcuerdo>
{
    public Guid Id { get; set; }
    public long AsuntoNeunId { get; set; }
    public int AsuntoDocumentoId { get; set; }
    public int TipoUpdate { get; set; }
    public long Valor { get; set; }
    public int Estado { get; set; }
    public string NombreDocumento { get; set; }

    public string TipoAsunto { get; set; }
    public string TipoProcedimiento { get; set; }
    public string NumeroExpediente { get; set; }
    public string Mesa { get; set; }
    public string SecretarioId { get; set; }
    public string NumeroPromocion { get; set; }
    public bool EnviarAlerta { get; set; }
}
