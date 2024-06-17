using System;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using DocumentFormat.OpenXml.Presentation;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerPromocion;
public class ObtenerArchivosHandler : IRequestHandler<ObtenerArchivosConsulta, ArchivosPromocion>
{
    private readonly IPromocionesRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;

    public ObtenerArchivosHandler(IPromocionesRepository repository, IMapper mapper, ISesionService sesionService)
    {
        _repository = repository;
        _mapper = mapper;
        _sesionService = sesionService;
    }

    public async Task<ArchivosPromocion> Handle(ObtenerArchivosConsulta request, CancellationToken cancellationToken)
    {
        var archivosYAnexos = new ArchivosPromocion();
        if (request.Origen != 0 && request.kIdElectronica !=null) // Toda promocion electronica se envia kIElectronica en lugar de AsuntoNeunId
        {
            request.AsuntoNeunId = (long)request.kIdElectronica;
        }
        var archivos = await _repository.ObtenerArchivosyAnexos(request.AsuntoNeunId, request.NumeroOrden, request.YearPromocion, _sesionService.SesionActual.CatOrganismoId, request.Origen,request.TipoModulo,request.AsuntoDocumentoId);
        foreach (var item in archivos)
        {
           var documento = new Documento() {
               Nombre = item.NombreArchivo,
               guidDocumento = item.guidDocumento.HasValue ? item.guidDocumento.Value.ToString() : null
           };
            
            if (item.EsElectronica == 1 && item.EsBoletaOCC != 1)
            {
                archivosYAnexos.Electronicos.Add(documento);
                documento.Descripcion = "Promoción";
            }
            else if (item.EsPromocion == 1 && item.EsBoletaOCC != 1)
            {
                archivosYAnexos.Archivos.Add(documento);
                documento.Descripcion = "Promoción";
            }
            else if(item.EsBoletaOCC != 1)
            {
                archivosYAnexos.Anexos.Add(documento);
                documento.Descripcion = item.DescripcionAnexo;
            }

            
        }

        return archivosYAnexos; // Devolver objeto con datos
    }


}
