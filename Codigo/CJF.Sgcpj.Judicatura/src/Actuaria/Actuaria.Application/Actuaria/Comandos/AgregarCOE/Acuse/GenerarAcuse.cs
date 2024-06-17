using Aspose.Words;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarCOE.Acuse;
public class GenerarAcuse : IGenerarAcuse
{
    private readonly IConfiguration _configuration;
    private readonly IDocumentoBlob _documentoBlob;

    public GenerarAcuse(IConfiguration configuration, IDocumentoBlob documentoBlob)
    {
        _configuration = configuration;
        _documentoBlob = documentoBlob;

        AsposeLicense licencia = new AsposeLicense(_configuration);
        Aspose.Words.License wordsLicencia = new Aspose.Words.License();
        wordsLicencia.SetLicense(licencia.GetLicense());
    }

    public string GeneraAcuseCOE(GeneraAcuseCOERequest request)
    {
        var response = string.Empty;

        var plantilla = obtenerPlantillaAcuseCoe("PlantillaAcuseOficio.docx");

        Document docWord = null;

        using (MemoryStream stream = new MemoryStream(plantilla))
        {

            docWord = new Document(stream);

            docWord.Range.Replace("@NombreOrgano", request.NombreOrgano);
            docWord.Range.Replace("@Correspondencia", request.Correspondencia);
            docWord.Range.Replace("@Folio", request.Folio);
            docWord.Range.Replace("@FechaEnvio", request.FechaEnvio);
            docWord.Range.Replace("@NumeroExpediente", request.NumeroExpediente);
            docWord.Range.Replace("@TipoAsunto", request.TipoAsunto);
            docWord.Range.Replace("@Parte", "Cambio de Parte Prueba");

            using (MemoryStream ms = new MemoryStream())
            {
                docWord.Save(ms, Aspose.Words.SaveFormat.Pdf);
                ms.Position = 0L;
                byte[] PDFBytes = new byte[ms.Length];
                ms.Read(PDFBytes, 0, PDFBytes.Length);
                ms.Close();
                response = Convert.ToBase64String(PDFBytes);
            }
        }


        return response;
    }

    private byte[] obtenerPlantillaAcuseCoe(string plantilla)
    {
        return _documentoBlob.ObtenerBlobDocumento(plantilla, _configuration["SISE3:BackEnd:OficiosPlantillasContenedor"],
            _configuration["SISE3:BackEnd:SMTPTemplatesUrl"]).ConfigureAwait(false).GetAwaiter().GetResult();
    }
}
