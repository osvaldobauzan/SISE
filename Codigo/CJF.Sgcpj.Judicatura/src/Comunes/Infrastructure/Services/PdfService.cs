using Aspose.Pdf;
using Aspose.Pdf.Facades;
using CJF.Sgcpj.Judicatura.Application.Utils;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;

namespace CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;

public class PdfService : IPdfService
{
    public PdfService(AsposeLicense licence)
    {
        Aspose.Words.License license = new Aspose.Words.License();
        license.SetLicense(licence.GetLicense());
    }
    public byte[] AddSings(byte[] data, string[] firmas)
    {
        if (data == null)
            throw new ArgumentNullException();

        var streamDocument = Functions.MallocFromArrayBytes(data);

        Aspose.Pdf.Document doc = new Aspose.Pdf.Document(streamDocument);
        FormattedText text = new FormattedText(firmas[0]);

        for (int i = 1; i < firmas.Length; i++)
        {
            string? item = firmas[i];
            text.AddNewLineText(item);
        }
        
        TextStamp textStamp = new TextStamp(text);
        textStamp.TextState.FontSize = 5;
        // Set properties of the stamp
        textStamp.TopMargin = 0;
        textStamp.LeftMargin = 10;
        textStamp.Rotate = Aspose.Pdf.Rotation.on270;
        textStamp.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Blue);
        textStamp.HorizontalAlignment = Aspose.Pdf.HorizontalAlignment.Left;
        textStamp.VerticalAlignment = VerticalAlignment.Center;

        // Add footer on all pages
        foreach (Page page in doc.Pages)
        {
            page.AddStamp(textStamp);
        }

        var stream = new MemoryStream();
        doc.Save(stream);
        return Functions.ConvertStreamToByteArray(stream);
    }
}