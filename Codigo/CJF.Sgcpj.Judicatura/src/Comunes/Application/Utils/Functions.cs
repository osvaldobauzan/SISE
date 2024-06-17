namespace CJF.Sgcpj.Judicatura.Application.Utils;

using System.Text;
using Microsoft.Extensions.Configuration;
using CJF.Sgcpj.Judicatura.Common.Domain.Common;

public class Functions
{
    private readonly IConfiguration _configuration;
    public Functions(IConfiguration configuration) =>
        (_configuration) = (configuration);

    public static long TamanioArchivoEnMB(int tamanioEnBytes) =>
        (tamanioEnBytes / (1024 * 1024));

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
        byte[] buffer = new byte[16 * 1024];
        stream.Position = 0;
        using (MemoryStream ms = new MemoryStream())
        {
            int read;
            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }
            return ms.ToArray();
        }
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
}
