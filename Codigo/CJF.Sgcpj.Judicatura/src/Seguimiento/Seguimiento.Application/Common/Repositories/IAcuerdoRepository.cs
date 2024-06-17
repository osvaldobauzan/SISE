using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Repositories;
/// <summary>
/// //////////INTERFASE PARA ACUERDO DE SEGUIMIENTO//////////////////////////////////
/// </summary>
public interface IAcuerdoRepository
{
    Task<Acuerdo> getAcuerdo(Acuerdo acuerdo);

    Task<int> getOrden(Acuerdo acuerdo);
}