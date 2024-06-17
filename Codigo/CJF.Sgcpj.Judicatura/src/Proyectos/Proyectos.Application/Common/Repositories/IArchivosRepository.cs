using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerArchivos;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Repositories;

public interface IArchivosRepository
{
    Task<string> RutaEscrituraPorModulo(string modulo);

    Task<List<ArchivoDto>> ObtenerArchivoDTO(long id);

    Task<string> RutaEscrituraPorModuloHistorico(string modulo);

    Task<List<RutasNas>> RutasPorModuloHistorico(string modulo);
}
