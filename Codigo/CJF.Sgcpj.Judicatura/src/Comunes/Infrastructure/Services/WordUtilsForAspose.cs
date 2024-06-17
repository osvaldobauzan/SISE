using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Properties;
using Aspose.Words.Replacing;
using CJF.Sgcpj.Judicatura.Application.Utils;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Common;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using Microsoft.Extensions.Configuration;
using Microsoft.IO;

namespace CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;

public class WordUtilsForAspose : IWordsUtil
{

    private readonly IGeneradorQR _generadorQR;
    private readonly IConfiguration _configuration;
    private readonly IDocumentoBlob _documentoBlob;

    public WordUtilsForAspose(IGeneradorQR generadorQR, IConfiguration configuration,
                              IDocumentoBlob documentoBlob, AsposeLicense licence)
    {
        _generadorQR = generadorQR;
        _configuration = configuration;
        _documentoBlob = documentoBlob;

        License license = new License();
        license.SetLicense(licence.GetLicense());
        Aspose.Pdf.License licensePfd = new Aspose.Pdf.License();
        licensePfd.SetLicense(licence.GetLicense());
    }


    public byte[] InsertQRCodeInWordDocument(byte[] data, string qrToRemplace, string qrCodeText, int pixelsPerQRModule)
    {
        int PosX = 1;
        int PosY = 1;
        int iWidth = 100;
        int iHeight = 100;

        if (data == null)
            throw new Exception("Falló");

        if (string.IsNullOrEmpty(qrCodeText))
            throw new ArgumentNullException("Codigo QR requerido.");

        if (pixelsPerQRModule < 3)
            throw new Exception("Longitud minima para generar el código QR fallida.");

        var uri = _configuration["SISE3:BackEnd:SMTPTemplatesUrl"];
        var contenedorPlantillas = _configuration["SISE3:BackEnd:OficiosPlantillasRecursosContenedor"];
        var identificadorPlantillaSISE = qrToRemplace.Trim() + ".png";
        var logoPng = _documentoBlob.ObtenerBlobDocumento(identificadorPlantillaSISE, contenedorPlantillas, uri).ConfigureAwait(false).GetAwaiter().GetResult();

        var byteQR = _generadorQR.GenerarQRWithLogo(qrCodeText, pixelsPerQRModule, logoPng);
        var streamImageQR = UtilsImages.ImageFromRawBgraArray(byteQR);
        var streamDocument = Functions.MallocFromArrayBytes(data);

        Document doc = new Document(streamDocument);

        DocumentBuilder builder = new DocumentBuilder(doc);
        NodeCollection shapes = doc.GetChildNodes(NodeType.Shape, true);
        foreach (Shape nshape in shapes)
        {
            if (nshape.AlternativeText.Contains(qrToRemplace))
            {
                nshape.ImageData.SetImage(streamImageQR);
            }
        }

        var streamModify = new MemoryStream();
        doc.Save(streamModify, SaveFormat.Docx);
        streamModify.Seek(0, SeekOrigin.Begin);

        return Functions.ConvertStreamToByteArray(streamModify);
    }

    public byte[] RemoveImage(byte[] data, string imageToRemove)
    {
        var streamDocument = Functions.MallocFromArrayBytes(data);
        Document doc = new Document(streamDocument);

        NodeCollection shapes = doc.GetChildNodes(NodeType.Shape, true);
        foreach (Shape nshape in shapes)
        {
            if (nshape.AlternativeText.Contains(imageToRemove))
            {
                nshape.Remove();
            }
        }

        var streamModify = new MemoryStream();
        doc.Save(streamModify, SaveFormat.Docx);
        streamModify.Seek(0, SeekOrigin.Begin);

        return Functions.ConvertStreamToByteArray(streamModify);
    }

    public byte[] ReplaceTextInDocx(byte[] data, string searchText, string replaceText)
    {
        if (data == null)
            throw new ArgumentNullException();

        if (string.IsNullOrEmpty(searchText))
            throw new ArgumentNullException();

        var streamDocument = Functions.MallocFromArrayBytes(data);

        Document doc = new Document(streamDocument);

        DocumentBuilder builderDoc = new DocumentBuilder(doc);
        doc.Range.Replace(searchText, replaceText, new FindReplaceOptions());

        var streamModify = new MemoryStream();
        doc.Save(streamModify, SaveFormat.Docx);
        streamModify.Seek(0, SeekOrigin.Begin);

        return Functions.ConvertStreamToByteArray(streamModify);
    }
    public byte[] ConvertDocToPdf(byte[] doc)
    {
        if (doc == null)
            throw new ArgumentNullException();
        var streamDocument = Functions.MallocFromArrayBytes(doc);

        Document document = new Document(streamDocument);
        var streamModify = new MemoryStream();
        document.Save(streamModify, SaveFormat.Pdf);
        streamModify.Seek(0, SeekOrigin.Begin);

        return Functions.ConvertStreamToByteArray(streamModify);
    }
    public byte[] MergePdf(List<byte[]> docs)
    {
        byte[] result = null;
        if (docs == null)
            throw new ArgumentNullException();

        int blockSize = 1024;
        int largeBufferMultiple = 1024 * 1024;
        int maximumBufferSize = 21 * largeBufferMultiple;

        var option = new RecyclableMemoryStreamManager.Options(blockSize, largeBufferMultiple, maximumBufferSize, (200 * blockSize), (maximumBufferSize * 8));

        var manager = new RecyclableMemoryStreamManager(option);

        var pdfFinal = new Aspose.Pdf.Document();

        foreach (var archivo in docs)
        {
            using (var archivoPDF = new Aspose.Pdf.Document(new MemoryStream(archivo)))
            {
                pdfFinal.Pages.Add(archivoPDF.Pages);
            }
        }
        foreach (Aspose.Pdf.Page page in pdfFinal.Pages)
        {
            int idx = 1;
            foreach (Aspose.Pdf.XImage image in page.Resources.Images)
            {

                using (RecyclableMemoryStream imageStream = new RecyclableMemoryStream(manager))
                {
                    if (imageStream.Length > 0)
                    {
                        image.Save(imageStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        page.Resources.Images.Replace(idx, imageStream, 15);
                        idx = idx + 1;
                    }
                }
            }
        }
        using (RecyclableMemoryStream memoryStream = new RecyclableMemoryStream(manager))
        {
            pdfFinal.Save(memoryStream, Aspose.Pdf.SaveFormat.Pdf);
            var tamanio = memoryStream.Length;
            result = memoryStream.GetBuffer();
        }
        return result;
    }

    public byte[] ModifyDocumentProperties(byte[] data, WordProperties properties)
    {
        var streamDocument = Functions.MallocFromArrayBytes(data);

        Document doc = new Document(streamDocument);

        BuiltInDocumentProperties docProperties = doc.BuiltInDocumentProperties;
        docProperties.Version = properties.Version;
        docProperties.Author = properties.Creator;
        docProperties.Company = "SISE3";
        docProperties.LastSavedTime = DateTime.Now;

        var streamModify = new MemoryStream();
        doc.Save(streamModify, SaveFormat.Docx);
        streamModify.Seek(0, SeekOrigin.Begin);

        return Functions.ConvertStreamToByteArray(streamModify);
    }
}
