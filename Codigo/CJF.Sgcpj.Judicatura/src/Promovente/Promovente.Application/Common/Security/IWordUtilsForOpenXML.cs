namespace CJF.Sgcpj.Judicatura.Promovente.Application.Common.Security;

public interface IWordUtilsForOpenXML
{
    byte[] ReplaceTextInDocx(byte[] data, string searchText, string replaceText);
    byte[] InsertImgInDocx(byte[] data, string imgBase64);
    byte[] InsertQRCodeInDocx(byte[] data, string valueTextInCodeQR, int pixelsPerQRModule);
}
