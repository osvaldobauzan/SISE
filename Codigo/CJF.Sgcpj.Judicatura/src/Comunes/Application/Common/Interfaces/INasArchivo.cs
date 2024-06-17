using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
public interface INasArchivo
{
    DocumentoBase64Dto ObtenerArchivoComoBase64String(string uncPath);
    public void AlmacenarArchivo(string uncPath, byte[] data);
}
