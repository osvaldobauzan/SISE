using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.DiasInhabiles.Consulta;
public class CatalogosDiasInhabilesFiltro: IRequest<List<CatalogoDiasInhabilesDto>>
{
    public DateTime FechaInicio { get; set; }
    public DateTime FechaFin{ get; set; }

}
