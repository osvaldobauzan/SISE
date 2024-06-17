using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Oficialia.Consultas.ObtenerPromociones;

namespace CJF.Sgcpj.Judicatura.Oficialia.Application.Common.Repositories;
public interface IOficialiaPartesRepository
{
    Task<List<OficialiaPartesDTO>> ObtenerPromociones(OficialiaPartesFiltro filtro);
}
