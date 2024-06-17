using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;
/// <summary>
///                         DECLARACION ENTIDADES PARA PROMOCIONES UNICAMENTE PARA SEGUIMIENTO
/// </summary>
public class Promociones : Documentos
{
    public int OrganismoId { get; set; }

    public int YearPromocion { get; set; }

    public int Orden { get; set; }

    public int CuadernoId { get; set; }

    public string Cuaderno { get; set; }
    public DateTime FechaPresentacion { get; set; }

    public string HoraPresentacion { get; set; }

    public string NumeroRegistro { get; set; }
}
