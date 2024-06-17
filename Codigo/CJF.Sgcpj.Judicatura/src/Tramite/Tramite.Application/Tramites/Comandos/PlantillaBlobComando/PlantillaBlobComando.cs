using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Comandos.PlantillaBlobComando;
public class PlantillaBlobComando : IRequest<bool>
{
    public PlantillaBlobDto Plantilla { get; set; }
}

public class PlantillaBlobComandoHandler : IRequestHandler<PlantillaBlobComando, bool>
{
    private readonly IMapper _mapper;
    private readonly IDocumentoBlob _documentoBlob;
    private readonly IConfiguration _configuration;
    private readonly IWordsUtil _wordsUtil;

    public PlantillaBlobComandoHandler(IMapper mapper, IDocumentoBlob documentoBlob, IConfiguration configuration, IWordsUtil wordsUtil)
    {
        _mapper = mapper;
        _documentoBlob = documentoBlob;
        _configuration = configuration;
        _wordsUtil = wordsUtil;
    }

    public async Task<bool> Handle(PlantillaBlobComando request, CancellationToken cancellationToken)
    {
        var resultado = false;
        var plantilla = _mapper.Map<PlantillaBlob>(request.Plantilla);
        var uri = _configuration["SISE3:BackEnd:SMTPTemplatesUrl"];
        var contenedorPlantillas = _configuration["SISE3:BackEnd:OficiosPlantillasContenedor"];
        var contenedorOficiosGenerados = _configuration["SISE3:BackEnd:OficiosContenedor"];

        var archivo = await _documentoBlob.ObtenerBlobDocumento(plantilla.ArchivoId, contenedorPlantillas, uri);


        IDictionary<string, string> valores = new Dictionary<string, string>();

        valores.Add("ASUNTO_ALIAS", "2422/2022");
        valores.Add("PROMOVENTE_NOMBRE", "Ismael González Sánchez");

        valores.Add("FECHA_ANEXO", "a diecinueve de enero de dos mil veintidós");
        valores.Add("SECRERARIO_NOMBRE_COMPLETO", "María del Rosario Jiménez Romero");


        IDictionary<string, string> qrs = new Dictionary<string, string>();

        qrs.Add("qrSISE", "{data:'{asuntoAlias:[" + valores["ASUNTO_ALIAS"] + "]}'}");


        foreach (var textoAReemplazar in valores)
        {
            archivo = _wordsUtil.ReplaceTextInDocx(archivo, textoAReemplazar.Key, textoAReemplazar.Value);
        }

        foreach (var qr in qrs)
        {
            archivo = _wordsUtil.InsertQRCodeInWordDocument(archivo, qr.Key, qr.Value, 10);
        }
        await _documentoBlob.GuardarBlobDocumento(archivo, $"Anexo{Guid.NewGuid()}.docx", contenedorOficiosGenerados, uri);

        resultado = true;

        return resultado;
    }
}
