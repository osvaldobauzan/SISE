
using AutoMapper;
using MediatR;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Repositories;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Seguimientos.Documento;

public record class DocumentosHandler : IRequest<Documentos>
{
    public Documentos documentos { get; set; }
}


public class ObtieneDocumentoHandler : IRequestHandler<DocumentosHandler, Documentos>
{
    private readonly IDocumentosRepository _documentosRepository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;

    public ObtieneDocumentoHandler(IDocumentosRepository documentosRepository, IMapper mapper, ISesionService sesionService)
    {
        _documentosRepository = documentosRepository;
        _mapper = mapper;
        _sesionService = sesionService;
    }

    public async Task<Documentos> Handle(DocumentosHandler request, CancellationToken cancellationToken)
    {

        var Result = await _documentosRepository.getDocumentos(request.documentos);
        return Result;
    }

}
