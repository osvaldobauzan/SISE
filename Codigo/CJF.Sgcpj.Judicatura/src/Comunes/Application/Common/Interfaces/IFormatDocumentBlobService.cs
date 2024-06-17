namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;

public interface IFormatDocumentBlobService
{
    Task<byte[]> ReplaceTextInBlobDocumentForAspose(string idSource, string textFromReplace, string textToReplace);
    Task<byte[]> ReplaceTextInBlobDocumentForOpenXML(string idSource, string textFromReplace, string textToReplace);
    Task<byte[]> InsertQRDocumentForAspose(string idSource, string qrCodeText, int pixelsPerQRModule, int PosX, int PosY, int iWidth, int iHeight);
    Task<byte[]> InsertQRDocumentForOpenXML(string idSource, string qrCodeText, int pixelsPerQRModule);
}
