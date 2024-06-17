using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Domain.Entities;
public class ContadoresEstadosTablero
{
    public int Total { get; set; }
    public int SinCaptura { get; set; }
    public int Capturadas { get; set; }
    public int Asignadas { get; set; }
}
