using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
public interface IGeneradorQR
{
    byte[] GenerarQr(string textoAEncodificar, int pixelesPorModulo);
    string GenerarQrToBase64(string textToEncode, int pixelsPerModule);
    byte[] GenerarQRWithLogo(string textToEncode, int pixelsPerModule, byte[] imgLogo);
}
