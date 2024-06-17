using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Repositories;
/// <summary>
/// //////////////INTERFASE PARA OFICIO DE SEGUIMIENTO ///////////////////////////////////////77
/// </summary>
public interface IOficioRepository
{


    Task<Oficio> getOficio(Oficio oficio);

    Task<Oficio> getFolioOficio(Oficio oficio);
}
