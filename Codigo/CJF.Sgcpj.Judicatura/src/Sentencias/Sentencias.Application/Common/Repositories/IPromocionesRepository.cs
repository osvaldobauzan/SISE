using CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Consultas.ObtenerPromociones;


namespace CJF.Sgcpj.Judicatura.Sentencias.Application.Common.Repositories;

public interface IPromocionesRepository
{
    Task<ListadoPromocionesDTO> ObtenerPromocionesCuaderno(int asuntoId, long asuntoNeunId, int tipoCuaderno, int sintesisOrden);
}
