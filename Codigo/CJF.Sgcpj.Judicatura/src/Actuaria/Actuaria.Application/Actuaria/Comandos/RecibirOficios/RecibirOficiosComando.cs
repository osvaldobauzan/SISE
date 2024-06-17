using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.RecibirOficios;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.RecibirOficios;
public class RecibirOficiosComando : IRequest<List<RecibirOficiosDto>>
{
   public List<RecibirOficiosDto> Oficios {  get; set; }
}
