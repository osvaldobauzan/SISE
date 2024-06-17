using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerAcuerdo;
public class ObtenerAcuerdoRequest : IRequest<List<ObtenerAcuerdoM>>
{
    public string FechaInicial { get; set; }
    public string FechaFinal { get; set; }
}
