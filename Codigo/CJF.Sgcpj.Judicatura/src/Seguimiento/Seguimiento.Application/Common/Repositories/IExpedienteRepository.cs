using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Repositories;
/// <summary>
/// ///////////////INTERFASE DE EXPEDIENTE DE SEUGUIMIENTO/////////////////////////////////
/// </summary>
public interface IExpedienteRepository
{
    Task<ExpedienteObtener> getExpediente(ExpedienteObtener expediente);

    Task<List<ExpedienteObtener>> getExpedientes(ExpedienteObtener expediente);

    Task<int> addAsunto(ExpedienteObtener expediente);
}
