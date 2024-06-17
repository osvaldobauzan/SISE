using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Validation;
using Microsoft.Extensions.Configuration;

using CJF.Sgcpj.Judicatura.Common.Domain.Common;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;

namespace CJF.Sgcpj.Judicatura.Promovente.Infrastructure.Services;

public class WordValidator : IWordValidator
{
    private readonly IConfiguration _configuration;
    private readonly IListSignatureFiles _signatureFiles;

    public WordValidator(IListSignatureFiles signatureFiles, IConfiguration configuration) =>
        (_signatureFiles, _configuration) = (signatureFiles, configuration);

    public bool isDocxValid(byte[] data, bool isVerifyXMLStructure = false)
    {
        try
        {
            using (MemoryStream stream = new MemoryStream(data))
            {
                using (var doc = WordprocessingDocument.Open(stream, false))
                {
                    if (!isVerifyXMLStructure)
                        return true;

                    try
                    {
                        var validator = new OpenXmlValidator();
                        var errors = validator.Validate(doc);
                        return errors.Any() ? false : true;
                    }
                    catch { return false; }
                }
            }
        }
        catch { return false; }
    }

    public bool IsValidSignatureFromDocx(string NombreArchivo, byte[] data)
    {
        if (data == null)
            return true;

        using (MemoryStream stream = new MemoryStream(data))
        {
            using (var reader = new BinaryReader(stream))
            {
                try
                {
                    var pathFile = Path.GetExtension(NombreArchivo).ToLower();
                    var signatures = _signatureFiles.GetFilesSignaturesValid()[pathFile];
                    var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

                    if (pathFile == Constants.SISE3_EXTENSIONWORD2007FILE)
                        return isDocxValid(data, false) && signatures.Any(signature => headerBytes.Take(signature.Length).SequenceEqual(signature));
                    else
                        return signatures.Any(signature => headerBytes.Take(signature.Length).SequenceEqual(signature));
                }
                catch { return false; }
            }
        }
    }

    public bool IsNameAndValidatedExtension(string nombreArchivo)
    {
        string extensionEsperada = _configuration[Constants.NAS_VALIDEXTENSIONFILESISE3NAST];
        string[] ExtensionesValidas = extensionEsperada.Split(",");
        return ExtensionesValidas.Any(ext => nombreArchivo.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
    }
}
