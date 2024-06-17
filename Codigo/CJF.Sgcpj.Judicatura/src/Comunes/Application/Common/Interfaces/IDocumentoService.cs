using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
public interface IDocumentoService
{
    Task<RespuestasLecturaDto?> ObtenerDocumentoBase64(string id);
}