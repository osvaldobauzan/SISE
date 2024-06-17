namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
public interface IDocumentoBlob
{
    Task<byte[]> ObtenerBlobDocumento(string fileId,  string contenedor, string uri);
    Task<string> ObtenerPlantillaCorreo(string fileId,  string contenedor, string uri);

    Task GuardarBlobDocumento(byte[] archivo, string fileId,  string contenedor, string uri);
    Task<string> ObtenerTextoBlob(string fileId, string contenedor, string uri);
}
