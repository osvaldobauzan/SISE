using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;

public class CatalogoAnexo
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public int Elementos { get; set; }
    public int CatTipoCatalogoAsuntoId { get; set; }
    public int CatTipoAsuntoId { get; set; }
}
