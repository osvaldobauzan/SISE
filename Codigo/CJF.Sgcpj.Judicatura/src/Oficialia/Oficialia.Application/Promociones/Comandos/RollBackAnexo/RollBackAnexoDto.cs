using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.RollBackAnexo;
public class RollBackAnexoDto : IMapFrom<Common.Models.RollBackAnexo>
{
    public long AsuntoNeunId { get; set; }
    public int AsuntoID { get; set; }
    public int YearPromocion { get; set; }
    public int NumeroOrden { get; set; }
    public int CatIdOrganismo { get; set; }
    public int RegistroEmpleadoId { get; set; }
    public int NumeroRegistro { get; set; }
    public int Consecutivo { get; set; }
    public int Origen { get; set; }
}
