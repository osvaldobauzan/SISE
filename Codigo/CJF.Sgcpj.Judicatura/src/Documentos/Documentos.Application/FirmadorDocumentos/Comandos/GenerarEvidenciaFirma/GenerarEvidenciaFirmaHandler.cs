
using System.Security.Cryptography.Pkcs;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using CJF.Firma.Util.Model;
using CJF.Firma.Util.Ocsp;
using CJF.Firma.Util.Sign;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
using Documentos.Application.Common.Model.GenerarEvidenciaFirma;
using MediatR;
using Org.BouncyCastle.Utilities.Encoders;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using Aspose.Words;
using System.Text.RegularExpressions;
using AngleSharp.Common;



namespace Documentos.Application.FirmadorDocumentos.Comandos.GenerarEvidenciaFirma;
public class GenerarEvidenciaFirmaHandler :  IRequestHandler<GenerarEvidienciaFirmaFiltro, GenerarEvidienciaFirmaDTO>
{
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    private readonly IHojaFirmasService _hojaFirmasService;
    private readonly IConfiguration _configuration;
    private readonly IDocumentoBlob _documentoBlob;
    public GenerarEvidenciaFirmaHandler(IMapper mapper, ISesionService sesionService, IHojaFirmasService hojaFirmasService)
    {
        _mapper = mapper;
        _sesionService = sesionService;
        _hojaFirmasService = hojaFirmasService;
    }
    public async Task<GenerarEvidienciaFirmaDTO> Handle(GenerarEvidienciaFirmaFiltro request, CancellationToken cancellationToken)
    {
        return _hojaFirmasService.GenerarHojaFirmas(request);
    }



   
    
}