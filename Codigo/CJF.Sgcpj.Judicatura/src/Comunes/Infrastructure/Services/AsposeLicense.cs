using System.Text;
using Microsoft.Extensions.Configuration;

namespace CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
public class AsposeLicense
{
    private MemoryStream _license;
    private string licenseKey;

    public AsposeLicense(IConfiguration configuration)
    {
        try
        {
            licenseKey = configuration["SISE3:BackEnd:AsposeTotalNETLIC"];
            _license = ConvertToMs(licenseKey);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public MemoryStream GetLicense()
    {
        return ConvertToMs(licenseKey);
    }

    static MemoryStream ConvertToMs(string cadena)
    {
        var bytes = Encoding.UTF8.GetBytes(cadena);
        var memoryStream = new MemoryStream(bytes);
        return memoryStream;

    }
}
