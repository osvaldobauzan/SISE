using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;
public class BaseCatalog
{
    public BaseCatalog()
    {
    }
    public BaseCatalog ShallowCopy()
    {
        return (BaseCatalog)MemberwiseClone();
    }
}
