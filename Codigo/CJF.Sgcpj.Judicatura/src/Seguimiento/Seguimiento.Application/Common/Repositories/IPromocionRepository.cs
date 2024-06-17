using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Repositories;
/// <summary>
/// ///////////////INTERFASE PARA PROMOCION DE SEGUIMIENTO /////////////////////////////////////77
/// </summary>
public interface IPromocionRepository
{
    Task<Models.Promociones> getOrdenPromocion(Models.Promociones promocion);

    Task<Models.Promociones> getPromocion(Models.Promociones promocion);
}