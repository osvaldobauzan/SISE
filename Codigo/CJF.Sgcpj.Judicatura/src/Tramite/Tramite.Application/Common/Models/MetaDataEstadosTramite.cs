using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;
public class MetaDataEstadosTramite
{
    public int TotalTramites { get; set; }
    public int TotalSinAcuerdo { get; set; }
    public int TotalCancelados { get; set; }
    public int TotalConAcuerdo { get; set; }
    public int TotalPreAutorizados { get; set; }
    public int TotalAutorizados { get; set; }
}
