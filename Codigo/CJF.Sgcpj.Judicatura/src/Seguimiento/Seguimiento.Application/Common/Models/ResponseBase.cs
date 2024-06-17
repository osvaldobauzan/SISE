using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;
public class ResponseBase
{
    public bool Success { get; set; }

    public Paging Paging { get; set; }

    public ErrorList ErrorList { get; set; }

    public ResponseBase()
    {
        Success = true;
    }
}
