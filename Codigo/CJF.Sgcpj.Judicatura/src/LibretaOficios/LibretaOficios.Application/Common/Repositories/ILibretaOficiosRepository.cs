using CJF.Sgcpj.Judicatura.LibretaOficios.Application.Common.Models;
namespace CJF.Sgcpj.Judicatura.LibretaOficios.Application.Common.Repositories;
public interface ILibretaOficiosRepository
{
    Task<List<LibretaOficio>> ObtenerLibretaOficio(LibretaOficioFiltro param);
    Task<bool> CancelarOficio(CancelarOficioFiltro param);
    Task<bool> InsertarOficio(InsertarOficioFiltro param);
}

