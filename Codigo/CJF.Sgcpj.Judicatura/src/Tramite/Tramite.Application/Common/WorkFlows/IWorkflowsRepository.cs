using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Common.WorkFlows;
public interface IWorkflowsRepository
{
    Task<string> ObtenerFlujoDeTrabajo(string nombreFlujo);
}
