using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Seguimientos.InsertarSeguimiento.Comando;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Repositories;


namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Repositories;
          
public interface ISeguimientoRepository
{ 
    Task<int> InsertarSeguimientoConFiltro(Models.Seguimiento seguimiento);

    Task<List<Models.Seguimiento>> getByList(Models.Seguimiento seguimiento);

    Task<IEnumerable<Models.Seguimiento>> getAllExpediente(Models.Seguimiento seguimiento);

    Task<List<Models.Seguimiento>> getBusca(Models.Seguimiento seguimiento);

    Task<List<Models.Seguimiento>> getCombExp(Models.Seguimiento seguimiento);

    Task<List<Models.Seguimiento>> getCombAsunto(Models.Seguimiento seguimiento);

     Task<List<Models.Seguimiento>> getCombPartes(Models.Seguimiento seguimiento);

     Task<List<Models.Seguimiento>> getTipoAsuntos(Models.Seguimiento seguimiento);

}




