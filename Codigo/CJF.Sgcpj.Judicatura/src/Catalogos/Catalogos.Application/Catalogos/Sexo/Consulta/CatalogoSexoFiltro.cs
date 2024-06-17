using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.Sexo.Consulta;
public class CatalogoSexoFiltro: IRequest<List<CatalogoSexoDTO>>
{ 
    public int KIdSexo { get; set; }
}
