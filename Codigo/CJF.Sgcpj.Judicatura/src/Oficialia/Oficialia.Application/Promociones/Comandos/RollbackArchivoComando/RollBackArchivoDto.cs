﻿using CJF.Sgcpj.Judicatura.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.RollbackArchivoComando;

public class RollBackArchivoDto : IMapFrom<RollBackArchivo>
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
