using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Promovente.Application.Common.Models;

public class BuscaAutoridadJudicial
{
    public string NombreCompleto { get; set; }
    public long EmpleadoId { get; set; }
    public string CargoDescripcion { get; set; }
    public int CatOrganismoId { get; set; }
    public string NombreOficial { get; set; }
}
