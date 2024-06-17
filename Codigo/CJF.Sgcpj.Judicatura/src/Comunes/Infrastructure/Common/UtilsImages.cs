using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;

namespace CJF.Sgcpj.Judicatura.Common.Infrastructure.Common;
public static class UtilsImages
{
    public static Stream ImageFromRawBgraArray(byte[] arr)
    {
        var outputStream = new MemoryStream();
        var image = Image.Load<Rgba32>(arr);
        image.SaveAsJpeg(outputStream, new JpegEncoder() { Quality = 100 });
        return outputStream;
    }
}
