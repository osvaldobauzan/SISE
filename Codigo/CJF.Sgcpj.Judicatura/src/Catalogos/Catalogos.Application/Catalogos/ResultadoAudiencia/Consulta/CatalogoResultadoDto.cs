using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.ResultadoAudiencia.Consulta;
public class CatalogoResultadoDto : IMapFrom<CatalogoResultado>
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
}
