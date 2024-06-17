using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;

using Aspose.Words;
using Aspose.Words.Drawing;
using CJF.Sgcpj.Judicatura.Application.Utils;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Common;

namespace CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
public class AcuerdosDocxsHelpers : IAcuerdosDocxsHelpers
{
    private readonly IGeneradorQR _generadorQR;

    public AcuerdosDocxsHelpers(IGeneradorQR generadorQR, AsposeLicense licence)
    {
        _generadorQR = generadorQR;
        Aspose.Words.License license = new Aspose.Words.License();
        license.SetLicense(licence.GetLicense());
    }

    public byte[] InsertarQrLateral(byte[] data, string qrCodeText, int pixelsPerQRModule)
    {
        if (data == null)
            throw new Exception("Archivo requerido");

        if (string.IsNullOrEmpty(qrCodeText))
            throw new ArgumentNullException("Codigo QR requerido.");

        if (pixelsPerQRModule < 3)
            throw new Exception("Longitud minima para generar el código QR fallida.");

        var streamDocument = Functions.MallocFromArrayBytes(data);
        var byteQR = _generadorQR.GenerarQr(qrCodeText, pixelsPerQRModule);
        Document doc = new Document(streamDocument);

        var streamImageQR = UtilsImages.ImageFromRawBgraArray(byteQR);

        DocumentBuilder builder = new DocumentBuilder(doc);

        Section currentSection = builder.CurrentSection;
        PageSetup pageSetup = currentSection.PageSetup;

        pageSetup.DifferentFirstPageHeaderFooter = false;
        pageSetup.OddAndEvenPagesHeaderFooter = true;

        pageSetup.FooterDistance = 20;
        builder.MoveToHeaderFooter(HeaderFooterType.FooterPrimary);

        builder.InsertImage(streamImageQR, RelativeHorizontalPosition.Page, 20,
            RelativeVerticalPosition.Page, 650, 50, 50, WrapType.Through);

        var streamModify = new MemoryStream();
        doc.Save(streamModify, SaveFormat.Docx);
        streamModify.Seek(0, SeekOrigin.Begin);

        return Functions.ConvertStreamToByteArray(streamModify);
    }
}
