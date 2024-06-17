using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.AsociaExpediente;

public record AsociarExpediente : IRequest<List<AsociarExpedienteDto>>
{
    public string AsuntoAlias { get; set; }
    public int CatOrganismoId { get; set; }
    public int? CatTipoAsuntoId { get; set; }
    public int? Modulo { get; set; }
    public int? CatTipoProcedimiento { get; set; }
    public string? TipoProcedimiento { get; set; }
}
