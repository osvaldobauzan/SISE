namespace CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;

using CJF.Sgcpj.Judicatura.Application.Utils;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;

public class WordUtilsForOpenXML : IWordsUtil
{
    private readonly IGeneradorQR _generadorQR;

    public WordUtilsForOpenXML() { }

    public WordUtilsForOpenXML(IGeneradorQR generadorQR)
    {
        _generadorQR = generadorQR;
    }

    public byte[] ReplaceTextInDocx(byte[] data, string searchText, string replaceText)
    {
        if (data == null)
            throw new Exception("Falló");

        if (string.IsNullOrEmpty(searchText))
            throw new ArgumentNullException(nameof(searchText));

        var streamDocument = Functions.MallocFromArrayBytes(data);
        using (var doc = WordprocessingDocument.Open(streamDocument, true))
        {
            var body = doc.MainDocumentPart.Document.Body;

            foreach (var text in body.Descendants<Text>())
            {
                if (text.Text.Contains(searchText))
                    text.Text = text.Text.Replace(searchText, replaceText);
            }
        }
        return data;
    }

    private static void AddImageToBody(WordprocessingDocument wordDoc, string relationshipId)
    {
        double englishMetricUnitsPerInch = 914400;
        double pixelsPerInch = 96;

        //calculate size in emu
        double emuWidth = 400 * englishMetricUnitsPerInch / pixelsPerInch;
        double emuHeight = 400 * englishMetricUnitsPerInch / pixelsPerInch;

        // Define the reference of the image.
        var element =
             new Drawing(
                 new DW.Inline(
                     new DW.Extent() { Cx = (Int64Value)emuWidth, Cy = (Int64Value)emuHeight },
                     new DW.EffectExtent()
                     {
                         LeftEdge = 0L,
                         TopEdge = 0L,
                         RightEdge = 0L,
                         BottomEdge = 0L
                     },
                     new DW.DocProperties()
                     {
                         Id = (UInt32Value)1U,
                         Name = "Picture 1"
                     },
                     new DW.NonVisualGraphicFrameDrawingProperties(
                         new A.GraphicFrameLocks() { NoChangeAspect = true }),
                     new A.Graphic(
                         new A.GraphicData(
                             new PIC.Picture(
                                 new PIC.NonVisualPictureProperties(
                                     new PIC.NonVisualDrawingProperties()
                                     {
                                         Id = (UInt32Value)0U,
                                         Name = "New Bitmap Image.jpg"
                                     },
                                     new PIC.NonVisualPictureDrawingProperties()),
                                 new PIC.BlipFill(
                                     new A.Blip(
                                         new A.BlipExtensionList(
                                             new A.BlipExtension()
                                             {
                                                 Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                             })
                                     )
                                     {
                                         Embed = relationshipId,
                                         CompressionState =
                                         A.BlipCompressionValues.Print
                                     },
                                     new A.Stretch(
                                         new A.FillRectangle())),
                                 new PIC.ShapeProperties(
                                     // Aqui se define ancho, largo y posicion de la imagen.
                                     new A.Transform2D(
                                         new A.Offset() { X = 450100L, Y = 500101L },
                                         new A.Extents() { Cx = (Int64Value)emuWidth, Cy = (Int64Value)emuHeight }),
                                     new A.PresetGeometry(
                                         new A.AdjustValueList()
                                     )
                                     { Preset = A.ShapeTypeValues.Rectangle }))
                         )
                         { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
                 )
                 {
                     DistanceFromTop = (UInt32Value)0U,
                     DistanceFromBottom = (UInt32Value)0U,
                     DistanceFromLeft = (UInt32Value)0U,
                     DistanceFromRight = (UInt32Value)0U,
                     EditId = "50D07946"
                 });

        // Append the reference to body, the element should be in a Run.
        wordDoc.MainDocumentPart.Document.Body.AppendChild(new Paragraph(new Run(element)));
    }

    public byte[] InsertQRCodeInWordDocument(byte[] data, string qrToRemplace, string qrCodeText, int pixelsPerQRModule)
    {
        if (data == null)
            throw new Exception("Falló");

        if (string.IsNullOrEmpty(qrCodeText))
            throw new ArgumentNullException(nameof(qrCodeText));

        var byteQR = _generadorQR.GenerarQr(qrCodeText, pixelsPerQRModule);

        var streamDocument = Functions.MallocFromArrayBytes(data);

        using (var doc = WordprocessingDocument.Open(streamDocument, true))
        {
            var body = doc.MainDocumentPart;
            ImagePart imagePart = body.AddImagePart(ImagePartType.Jpeg);

            var streamImageQR = Functions.MallocFromArrayBytes(byteQR);
            imagePart.FeedData(streamImageQR);

            AddImageToBody(doc, body.GetIdOfPart(imagePart));
        }
        return data;
    }


    public byte[] RemoveImage(byte[] data, string imageToRemove)
    {
        throw new NotImplementedException();
    }
    public byte[] ConvertDocToPdf(byte[] doc)
    {
        throw new NotImplementedException();
    }
    public byte[] MergePdf(List<byte[]> docs)
    {
        throw new NotImplementedException();
    }

    public byte[] ModifyDocumentProperties(byte[] data, WordProperties properties)
    {
        throw new NotImplementedException();
    }
}
