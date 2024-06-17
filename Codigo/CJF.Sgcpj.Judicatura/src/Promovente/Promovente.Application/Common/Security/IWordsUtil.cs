namespace CJF.Sgcpj.Judicatura.Promovente.Application.Common.Security;
public interface IWordsUtil
{
    byte[] ReplaceTextInDocx(byte[] data, string searchText, string replaceText);
    byte[] InsertQRCodeInWordDocument(byte[] data, string qrCodeText, int pixelsPerQRModule);
}
