using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Repositories;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerArchivos;
public class ObtenerArchivosHandler : IRequestHandler<ObtenerArchivosConsulta, ArchivosPromocionDto>
{
    private readonly ITramitesRepository _repository;
    private readonly ISesionService _sesionService;

    public ObtenerArchivosHandler(ITramitesRepository repository, ISesionService sesionService)
    {
        _repository = repository;
        _sesionService = sesionService;
    }

    public async Task<ArchivosPromocionDto> Handle(ObtenerArchivosConsulta request, CancellationToken cancellationToken)
    {
        var archivosYAnexos = new ArchivosPromocionDto();

        var archivos = await _repository.ObtenerArchivosyAnexos(request.AsuntoNeunId, request.NumeroOrden,
            request.YearPromocion, _sesionService.SesionActual.CatOrganismoId, request.Origen, request.TipoModulo, request.AsuntoDocumentoId);

        foreach (var item in archivos)
        {
            var documento = new Documento() 
            { 
                Nombre = item.NombreArchivo, 
                GuidDocumento = item.GuidDocumento
            };

            if (item.EsPromocion == 1)
            {
                archivosYAnexos.Archivos.Add(documento);
                documento.Descripcion = "Promoción";
            }
            else
            {
                archivosYAnexos.Anexos.Add(documento);
                documento.Descripcion = item.DescripcionAnexo;
            }
        }

        return archivosYAnexos;
    }
}
