using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using PolyCache.Cache;

namespace CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
public class DocumentoBlobService : IDocumentoBlob
{
    private readonly IStaticCacheManager _cache;
    private readonly IRecargarTemplatesService _recargarTemplatesService;

    public DocumentoBlobService(IStaticCacheManager cache, IRecargarTemplatesService recargarTemplatesService)
    {
        _cache = cache;
        _recargarTemplatesService = recargarTemplatesService;
    }
    public async Task<byte[]> ObtenerBlobDocumento(string fileId, string contenedor, string uri)
    {
        try
        {
            var blobClient = new BlobServiceClient(new Uri(uri), new DefaultAzureCredential());
            var blobContainer = blobClient.GetBlobContainerClient(contenedor);
            var blob = blobContainer.GetBlobClient(fileId);

            using var ms = new MemoryStream();
            await blob.DownloadToAsync(ms);
            return ms.ToArray();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task GuardarBlobDocumento(byte[] archivo, string fileId, string contenedor, string uri)
    {
        try
        {
            var blobClient = new BlobServiceClient(new Uri(uri), new DefaultAzureCredential());
            var blobContainer = blobClient.GetBlobContainerClient(contenedor);
            var blob = blobContainer.GetBlobClient(fileId);

            using var ms = new MemoryStream(archivo);
            await blob.UploadAsync(ms, true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task<string> ObtenerPlantillaCorreo(string template, string contenedor, string uri)
    {
        if (_recargarTemplatesService.EsPrimeraVez) 
        {
            await _cache.RemoveAsync(new CacheKey(LLaveEmail(template)));
            _recargarTemplatesService.EsPrimeraVez = false;
        }

        int cacheExpiryTime = 800; //minutes

        return await _cache.GetWithExpireTimeAsync(
              new CacheKey(LLaveEmail(template)),
              cacheExpiryTime,
              async () => await ObtenerTemplate(template, contenedor,uri));
    }

    private async Task<string> ObtenerTemplate(string lLaveEmail, string contenedor, string uri)
    {
        var bytes = await ObtenerBlobDocumento(lLaveEmail, contenedor, uri);
        var templateEmail = System.Text.Encoding.Default.GetString(bytes);
        return templateEmail;
    }

    private string LLaveEmail(string template)
    {
        return "ema_" + template.Trim();
    }

    public async Task<string> ObtenerTextoBlob(string fileId, string contenedor, string uri)
    {
        try
        {
            var blobClient = new BlobServiceClient(new Uri(uri), new DefaultAzureCredential());
            var blobContainer = blobClient.GetBlobContainerClient(contenedor);
            var blob = blobContainer.GetBlobClient(fileId);


            BlobDownloadResult downloadResult = await blob.DownloadContentAsync();
            return downloadResult.Content.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
