namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
public interface IWordContenido
{
    string ReadDocumentWord(byte[] data);
    byte[] ReplaceHtmlInWordBookmark(byte[] data,string html);
    byte[] ReplaceHtmlInWord(byte[] data, string html);
    byte[] SaveWordtoPdf(byte[] data);

}
