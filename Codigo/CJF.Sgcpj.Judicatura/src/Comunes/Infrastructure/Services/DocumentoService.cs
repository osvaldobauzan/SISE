using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
public class DocumentoService : IDocumentoService
{
    private const string ENDPOINT_KEY = "SISE3:BackEnd:FirmadorEndPointLecturaDocumento";
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfiguration _configuration;
    private readonly ILogger<DocumentoService> _logger;

    public DocumentoService(IHttpClientFactory clientFactory,
        IConfiguration configuration, ILogger<DocumentoService> logger)
    {
        _clientFactory = clientFactory;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<RespuestasLecturaDto?> ObtenerDocumentoBase64(string id)
    {
        RespuestasLecturaDto? respuesta = null;
        try
        {
            var httpClientDocumentos = _clientFactory.CreateClient("DocumentosHttpClient");
            var response = await httpClientDocumentos.GetAsync($"{_configuration[ENDPOINT_KEY]}{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                respuesta = JsonConvert.DeserializeObject<RespuestasLecturaDto>(responseBody);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al obtener el documento. {ex.Message}");
        }

        return respuesta;
    }

}