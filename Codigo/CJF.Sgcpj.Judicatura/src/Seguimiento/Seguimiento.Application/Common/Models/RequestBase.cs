using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;
public class RequestBase
{
    public OperationType OperationType { get; set; }

    public string User { get; set; }

    public Paging Paging { get; set; }

    public RequestBase()
    {
        Paging = new Paging();
    }
}
