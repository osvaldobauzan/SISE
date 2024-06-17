using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerArchivos;

public record ObtenerArchivo : IRequest<DocumentoBase64Dto>
{
    public long Id { get; set; }

    public int TipoArchivo { get; set; }

    public bool Descargar { get; set; }
}
