using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Common.Infrastructure.Files;
public class NasArchivo : INasArchivo
{
    public void AlmacenarArchivo(string uncPath, byte[] data)
    {
        throw new NotImplementedException();
    }

    public DocumentoBase64Dto ObtenerArchivoComoBase64String(string uncPath)
    {
        var archivoBase64Dto = new DocumentoBase64Dto();

        byte[] pdfBytes;
        using (FileStream fileStream = new FileStream(uncPath, FileMode.Open, FileAccess.Read))
        {
            pdfBytes = new byte[fileStream.Length];
            fileStream.Read(pdfBytes, 0, pdfBytes.Length);
        }

        archivoBase64Dto.Base64 = Convert.ToBase64String(pdfBytes);

        return archivoBase64Dto;
    }
}
