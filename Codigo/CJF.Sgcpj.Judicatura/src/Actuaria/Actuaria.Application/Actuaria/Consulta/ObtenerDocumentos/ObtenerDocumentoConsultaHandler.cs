using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerDocumentos;
public class ObtenerDocumentoConsultaHandler : IRequestHandler<ObtenerDocumentoConsulta, DocumentoBase64Dto>
{
    private readonly ISesionService _sesionService;
    private readonly IActuariaRepository _repository;
    private readonly IRutasChunkService _rutasChunkService;

    public ObtenerDocumentoConsultaHandler(ISesionService sesionService,
                                           IActuariaRepository repository,
                                           IRutasChunkService rutasChunkService)
    {
        _sesionService = sesionService;
        _repository = repository;
        _rutasChunkService = rutasChunkService;
    }

    public async Task<DocumentoBase64Dto> Handle(ObtenerDocumentoConsulta request, CancellationToken cancellationToken)
    {
        
        var archivos = await _repository.ObtenerArchivosyAnexos(request.AsuntoNeunId, request.NumeroOrden,
            request.YearPromocion, _sesionService.SesionActual.CatOrganismoId, request.Origen, request.TipoModulo, request.AsuntoDocumentoId,
            request.SintesisOrden);

        //Buscar archivos
        var archivoBuscado = archivos.FirstOrDefault(s => s.RutaCompleta.EndsWith(request.NombreArchivo));

        if (archivoBuscado == null)
        {
            throw new DirectoryNotFoundException();
        }

        var archivo = await _rutasChunkService.RutasChunkPorModuloAsync(archivoBuscado.RutaCompleta, RutasChunkModulos.Acuses, request.AsuntoNeunId, request.YearPromocion
            , request.NumeroOrden, request.SintesisOrden);
        if (archivo != null)
        {
            archivo.NombreArchivo = request.NombreArchivo;
        }

        return archivo;

    }
}
