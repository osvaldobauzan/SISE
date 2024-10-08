﻿namespace CJF.Sgcpj.Judicatura.Application.Common.Models;

public class RollBackArchivo
{
    public long AsuntoNeunId { get; set; }
    public long AsuntoID { get; set; }
    public int YearPromocion { get; set; }
    public int NumeroOrden { get; set; }
    public int CatIdOrganismo { get; set; }
    public int RegistroEmpleadoId { get; set; }
    public int NumeroRegistro { get; set; }
    public int Consecutivo { get; set; }
    public int Origen { get; set; }
}
