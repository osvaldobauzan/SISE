using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
public interface IWordsUtil
{
    byte[] ReplaceTextInDocx(byte[] data, string searchText, string replaceText);
    byte[] InsertQRCodeInWordDocument(byte[] data, string qrToRemplace,string qrCodeText, int pixelsPerQRModule);
    byte[] RemoveImage(byte[] data, string imageToRemove);
    byte[] ConvertDocToPdf(byte[] doc);
    byte[] MergePdf(List<byte[]> docs);

    byte[] ModifyDocumentProperties(byte[] data, WordProperties properties);
}
