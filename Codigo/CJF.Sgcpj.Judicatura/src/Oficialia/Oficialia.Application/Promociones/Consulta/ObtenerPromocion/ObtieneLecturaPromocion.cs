using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Application.Cuadernos.Consulta;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtienePromocionesConFiltro;
using CJF.Sgcpj.Judicatura.Oficialia.Domain.Entities;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerPromocion;
public record ObtieneLecturaPromocion : IRequest<Promocion>
{
    public int AsuntoNeunId { get; set; }
    public int AsuntoID { get; set; }
    public int YearPromocion { get; set; }
    public int NumeroOrden { get; set; }
    public int CatIdOrganismo { get; set; }
    public int NumeroRegistro { get; set; }
    public int OrigenPromocion { get; set; }
}
