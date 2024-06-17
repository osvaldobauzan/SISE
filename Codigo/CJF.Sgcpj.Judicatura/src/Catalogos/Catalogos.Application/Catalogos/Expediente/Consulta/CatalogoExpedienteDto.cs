using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Expediente.Consulta;
public class CatalogoExpedienteDto : IMapFrom<CatalogoExpediente>
{
    public string NumeroExpediente { get; set; }
    public int CatTipoAsuntoId { get; set; }
}
