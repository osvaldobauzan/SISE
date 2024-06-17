using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.WorkFlows;
using Microsoft.Extensions.Configuration;
using PolyCache.Cache;

namespace CJF.Sgcpj.Judicatura.Tramite.Infrastructure.Persistence.Repositories;
public class WorkFlowsRepository : IWorkflowsRepository
{
    private IDocumentoBlob _documentoBlob;
    private IConfiguration _configuration;
    private readonly IStaticCacheManager _staticCacheManager;

    public WorkFlowsRepository(IDocumentoBlob documentoBlob, IConfiguration configuration, 
        IStaticCacheManager staticCacheManager)
    {
        _documentoBlob = documentoBlob;
        _configuration = configuration;
        _staticCacheManager = staticCacheManager;
    }
    public async Task<string> ObtenerFlujoDeTrabajo(string nombreFlujo)
    {
        string cachePrefix = "promociones_";
        int cacheExpiryTime = 800; //minutes

        return await _staticCacheManager.GetWithExpireTimeAsync(
              new CacheKey(LLaveWf(nombreFlujo)),
              cacheExpiryTime,

        async () => await ObtenrWf(nombreFlujo));
    }

    private async Task<string> ObtenrWf(string nombreFlujo)
    {
        var uri = _configuration["SISE3:BackEnd:SMTPTemplatesUrl"];
        var contenedorWorkflows = "workflows";

        var bytesWfJson = await _documentoBlob.ObtenerBlobDocumento(nombreFlujo + ".json", contenedorWorkflows, uri);
        Encoding encoding = Encoding.UTF8;
        var wfJson = encoding.GetString(bytesWfJson);
        return wfJson;
    }

    private string LLaveWf(string nombreFlujo)
    {
        return "wf_"+nombreFlujo.Trim();
    }
}
