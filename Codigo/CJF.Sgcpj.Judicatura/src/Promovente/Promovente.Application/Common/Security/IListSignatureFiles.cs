namespace CJF.Sgcpj.Judicatura.Promovente.Application.Common.Security;
public interface IListSignatureFiles
{
    Dictionary<string, List<byte[]>> GetFilesSignaturesValid();
}
