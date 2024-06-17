using CJF.Sgcpj.Judicatura.Common.Application.Common.Exceptions;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Common.Domain.Common;
using Ganss.Xss;

namespace CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
public class SanitizerService : ISanitizerService
{
    public string SanitizeHtml(string inputHtml)
    {
        try
        {
            var formatter = AngleSharp.Xhtml.XhtmlMarkupFormatter.Instance;
            HtmlSanitizer.DefaultOutputFormatter = formatter;
            var sanitizer = new HtmlSanitizer();

            sanitizer.AllowedSchemes.Add("data");
            AngleSharp.Css.Values.Color.UseHex = true;

            var sanitized = sanitizer.Sanitize(inputHtml);
            return sanitized;
        }
        catch (Exception e)
        {
            throw new XssDetectedException(Constants.SISE3_XSSDETECTED);
        }
    }
}
