using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Consultas.ObtenerSentencias;

using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerArchivos;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Sentencias.Application.Common.Repositories;

public interface IArchivosRepository
{
    Task<string> RutaEscrituraPorModulo(string modulo);

    Task<List<ArchivoSentenciaDto>> ObtenerArchivoDTO(long AsuntoNeunId, long AsuntoDocumentoId);

    Task<List<RutasNas>> RutasPorModuloHistorico(string modulo);
    Task<IEnumerable<SentenciaArchivo>> ObtenerSentenciaPorIdModuloTipoAsync(Guid id, int modulo, string tipoArchivo);
}
