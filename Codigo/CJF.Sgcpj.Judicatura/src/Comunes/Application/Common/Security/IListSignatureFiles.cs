namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
public interface IListSignatureFiles
{
    Dictionary<string, List<byte[]>> GetFilesSignaturesValid();
}
