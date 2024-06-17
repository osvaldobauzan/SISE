namespace CJF.Sgcpj.Judicatura.Tramite.Application.Utils;

using System.Text;
using SixLabors.ImageSharp.Formats.Jpeg;
using Microsoft.Extensions.Configuration;
using CJF.Sgcpj.Judicatura.Common.Domain.Common;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

public class Functions
{
    private readonly IConfiguration _configuration;
    public Functions(IConfiguration configuration) =>
        _configuration = configuration;

    public static long TamanioArchivoEnMB(int tamanioEnBytes) =>
        tamanioEnBytes / (1024 * 1024);

    public static byte[] StringToByteArray(string hex)
    {
        hex = hex.Replace("0x", string.Empty);
        int length = hex.Length / 2;
        byte[] bytes = new byte[length];
        for (int i = 0; i < length; i++)
        {
            bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
        }
        return bytes;
    }

    public static Stream MallocFromArrayBytes(byte[] bytesData)
    {
        var contentStream = new MemoryStream(0);
        contentStream.Write(bytesData, 0, bytesData.Length);
        return contentStream;
    }

    public static byte[] ConvertStreamToByteArray(Stream stream)
    {
        var ms = new MemoryStream(0);
        stream.CopyTo(ms);
        return ms.ToArray();
    }

    public static string GenerateStringRandom(int longitud)
    {
        const string caracteresPermitidos = Constants.SISE3_ALPHACOLLECTION;
        StringBuilder resultado = new StringBuilder();

        Random random = new Random();
        for (int i = 0; i < longitud; i++)
        {
            int indice = random.Next(caracteresPermitidos.Length);
            resultado.Append(caracteresPermitidos[indice]);
        }

        return resultado.ToString();
    }

    public static void BytesToLocalFile(byte[] bytesData, string Destination)
    {
        string carpetaDeLaAplicacion = AppDomain.CurrentDomain.BaseDirectory;
        string rutaCompleta = Path.Combine(carpetaDeLaAplicacion, Destination);
        File.WriteAllBytes(rutaCompleta, bytesData);
    }

    public static Stream ImageFromRawBgraArray(byte[] arr)
    {
        var outputStream = new MemoryStream();
        var image = Image.Load<Rgba32>(arr);
        image.SaveAsJpeg(outputStream, new JpegEncoder() { Quality = 100 });

        using (var fs = new FileStream(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{GenerateStringRandom(15)}.jpeg"), FileMode.Create, FileAccess.Write))
        {
            outputStream.WriteTo(fs);
        }

        return outputStream;
    }
}
