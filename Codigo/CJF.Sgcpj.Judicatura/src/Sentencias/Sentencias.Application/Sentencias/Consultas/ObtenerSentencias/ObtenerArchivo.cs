using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Consultas.ObtenerSentencias;

public class ObtenerArchivo : IRequest<DocumentoBase64Dto>
{
    public long AsuntoNeunId { get; set; }

    public long AsuntoDocumentoId { get; set; }
}
