using Microsoft.Extensions.Configuration;

using CJF.Sgcpj.Judicatura.Common.Domain.Common;
using CJF.Sgcpj.Judicatura.Application.Utils;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;

namespace CJF.Sgcpj.Judicatura.Promovente.Infrastructure.Services;
public class ListSignatureFiles : IListSignatureFiles
{
    private readonly string[] pdfHexaDecimalSignatures;
    private readonly IConfiguration _configuration;

    public ListSignatureFiles(IConfiguration configuration)
    {
        _configuration = configuration;
        pdfHexaDecimalSignatures = _configuration[Constants.SISE3_SIGNATUREHEXPDF].Split(",");
    }

    public Dictionary<string, List<byte[]>> GetFilesSignaturesValid()
    {

        List<byte[]> pdfByteArrays = new List<byte[]>();

        foreach (var hexSignature in pdfHexaDecimalSignatures)
        {
            pdfByteArrays.Add(Functions.StringToByteArray(hexSignature));
        }

        return new Dictionary<string, List<byte[]>>
        {
            {
                Constants.SISE3_EXTENSIONWORD2007FILE, new List<byte[]>
                {
                    Functions.StringToByteArray(_configuration[Constants.SISE3_SIGNATUREHEXDOCX])
                }
            },
            {
                Constants.SISE3_EXTENSIONPDFADOBEFILE,pdfByteArrays

             }
        };
    }
}
