using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
public interface IPrivilegiosService
{
    Task<List<Privilegio>> ObtenerConfiguracionPrivilegios();
}
