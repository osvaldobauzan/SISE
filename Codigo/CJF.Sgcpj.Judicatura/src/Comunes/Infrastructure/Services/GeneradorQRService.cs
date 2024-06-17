using CJF.Sgcpj.Judicatura.Application.Utils;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using QRCoder;
using QRCoder.ImageSharp;
using QRCoder.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
public class GeneradorQRService : IGeneradorQR
{
    public byte[] GenerarQr(string textoAEncodificar, int pixelesPorModulo)
    {
        QRCoder.BitmapByteQRCode df = new QRCoder.BitmapByteQRCode();

        QRCodeGenerator qRCodeGenerator = new();
        QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(textoAEncodificar, QRCodeGenerator.ECCLevel.M);
        PngByteQRCode qrCode = new(qRCodeData);
        return qrCode.GetGraphic(pixelesPorModulo);
    }

    public string GenerarQrToBase64(string textToEncode, int pixelsPerModule)
    {
        QRCodeGenerator qRCodeGenerator = new();
        QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(textToEncode, QRCodeGenerator.ECCLevel.M);
        PngByteQRCode qrCode = new(qRCodeData);
        return Convert.ToBase64String(qrCode.GetGraphic(pixelsPerModule));
    }

    public byte[] GenerarQRWithLogo(string textToEncode, int pixelsPerModule, byte[] imgLogo)
    {
        var outputStream = new MemoryStream();

        var gen = new QRCodeGenerator();
        var data = gen.CreateQrCode(textToEncode, QRCodeGenerator.ECCLevel.H);
        var qrcode = new ImageSharpQRCode(data);

        var imagen = Image.Load(imgLogo);

        var image = qrcode.GetGraphic(50,Color.Black,Color.White,Color.White, imagen, LogoLocation.Center, LogoBackgroundShape.Rectangle,drawQuietZones:false);
        image.SaveAsJpeg(outputStream, new JpegEncoder() { Quality = 100 });

        return Functions.ConvertStreamToByteArray(outputStream);
    }
}
