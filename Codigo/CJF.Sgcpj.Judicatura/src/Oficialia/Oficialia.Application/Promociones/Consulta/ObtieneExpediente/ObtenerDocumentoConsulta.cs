using CJF.Sgcpj.Judicatura.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtieneExpediente;
public record class ObtenerDocumentoConsulta : IRequest<DocumentoBase64Dto>
{
    public string NombreArchivo { get; set; }
    public long AsuntoNeunId { get; set; }
    public int YearPromocion { get; set; }
    public int NumeroOrden { get; set; }
    public int CatIdOrganismo { get; set; }
    public int Origen { get; set; }
    public int TipoModulo { get; set; }
    public int NumeroRegistro { get; set; }
    public int AsuntoDocumentoId { get; set; }
    public long? kIdElectronica { get; set; }
}


public class ObtieneExpedienteHandler : IRequestHandler<ObtenerDocumentoConsulta, DocumentoBase64Dto>
{
    private readonly ISesionService _sesionService;
    private readonly IPromocionesRepository _repository;
    private readonly IRutasChunkService _rutasChunkService;

    public ObtieneExpedienteHandler(ISesionService sesionService, 
                                    IPromocionesRepository repository,
                                    IRutasChunkService rutasChunkService)
    {
        _sesionService = sesionService;
        _repository = repository;
        _rutasChunkService = rutasChunkService;
    }

    public async Task<DocumentoBase64Dto> Handle(ObtenerDocumentoConsulta request, CancellationToken cancellationToken)
    {
        if (request.Origen != 0 && request.kIdElectronica != null) // Toda promocion electronica se envia kIElectronica en lugar de AsuntoNeunId
        {
            request.AsuntoNeunId = (long)request.kIdElectronica;
        }

        var archivos = await _repository.ObtenerArchivosyAnexos(request.AsuntoNeunId, request.NumeroOrden,
           request.YearPromocion, _sesionService.SesionActual.CatOrganismoId, request.Origen, request.TipoModulo, request.AsuntoDocumentoId);

        //Buscar archivos
        var archivoBuscado = archivos.FirstOrDefault(s => s.RutaCompleta.EndsWith(request.NombreArchivo));

        if (archivoBuscado == null)
        {
            throw new DirectoryNotFoundException();
        }

        return await _rutasChunkService.RutasChunkPorModuloAsync(archivoBuscado.RutaCompleta, RutasChunkModulos.Oficialia, request.AsuntoNeunId, request.YearPromocion, request.NumeroOrden);
    }


}

