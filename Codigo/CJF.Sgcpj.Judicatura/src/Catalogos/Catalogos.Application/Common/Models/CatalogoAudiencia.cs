using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Audiencia.Consulta;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
public class CatalogoAudiencia
{
    public int IdAudiencia { get; set; }
    public string Descripcion { get; set; }
    public int FAud { get; set; }
    public int HAud { get; set; }
    public int FDif { get; set; }
    public int HDif { get; set; }
    public int UsaPartes { get; set; }

}
