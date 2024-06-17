namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;

public interface IAcuerdosDocxsHelpers
{
    public byte[] InsertarQrLateral(byte[] data, string qrCodeText, int pixelsPerQRModule);
}
