using System.Runtime.InteropServices;

namespace CJF.Sgcpj.Judicatura.Promovente.Application.Common.Security;
public interface IWordValidator
{
    bool IsValidSignatureFromDocx(string NombreArchivo, byte[] data);
    bool isDocxValid(byte[] data, bool isVerifyXMLStructure = false);
    bool IsNameAndValidatedExtension(string nombreArchivo);
}
