using System.Text;
using Azure.Identity;
using Azure.Storage.Blobs;
using CJF.Sgcpj.Judicatura.Alertas.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Infrastructure.Services;

public class EmailTemplatesService : IEmailTemplatesService
{
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;

    public EmailTemplatesService(ILogger logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<(bool IsSuccess, string? Template, string? ErrorMessage)> GetTemplateAsync(string templateId)
    {
        try
        {
            var blobClient = new BlobServiceClient(
                new Uri($"{_configuration["SISE3:BackEnd:SMTPTemplatesUrl"]}"),
                        new DefaultAzureCredential());

            var container = blobClient.GetBlobContainerClient(_configuration["SISE3:BackEnd:SMTPTemplatesContainer"]);
            var blob = container.GetBlobClient(templateId);

            if(!await blob.ExistsAsync()) 
            {
                return (false, null, "Template not found");
            }

            using var ms = new MemoryStream();
            await blob.DownloadToAsync(ms);

            var template = Encoding.UTF8.GetString(ms.ToArray());
            return (true, template, null);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.Message);
            return (false, null, ex.Message);
        }
    }
}
