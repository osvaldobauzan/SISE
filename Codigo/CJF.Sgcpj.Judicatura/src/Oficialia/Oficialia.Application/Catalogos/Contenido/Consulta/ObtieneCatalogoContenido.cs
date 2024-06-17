using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Application.Cuadernos.Consulta;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Application.Catalogos.Contenido.Consulta;
public record ObtieneCatalogoContenido : IRequest<List<ContenidoDto>>
{
    public int CatTipoOrganismoId { get; set; }
    public int CatTipoAsuntoId { get; set; }
}
