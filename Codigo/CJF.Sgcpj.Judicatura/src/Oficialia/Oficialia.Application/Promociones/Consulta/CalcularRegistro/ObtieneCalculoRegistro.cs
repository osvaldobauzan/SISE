using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Application.Cuadernos.Consulta;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.CalcularRegistro;
public record ObtieneCalculoRegistro : IRequest<RegistroDto>
{

}
