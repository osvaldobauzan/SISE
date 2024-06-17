using System;
using System.Text.RegularExpressions;
using AngleSharp.Common;
using Aspose.Pdf.Operators;
using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Tables;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models.Actuario;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Common;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;
using Microsoft.Extensions.Configuration;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Actuario.GenerarAcuseOficio;
public class ServiceGeneracionOficio : IServiceGeneracionOficio
{
    private readonly IConfiguration _configuration;
    private readonly IDocumentoBlob _documentoBlob;
    private readonly IGeneradorQR _generadorQR;

    public ServiceGeneracionOficio(IConfiguration configuration, IDocumentoBlob documentoBlob, IGeneradorQR generadorQR)
    {
        _configuration = configuration;
        _documentoBlob = documentoBlob;
        _generadorQR = generadorQR;

        AsposeLicense licencia = new AsposeLicense(_configuration);
        Aspose.Words.License wordsLicencia = new Aspose.Words.License();
        wordsLicencia.SetLicense(licencia.GetLicense());
    }

    public string ObtenDocumento(List<ConsultaOficioActuario> listaItems)
    {
        string base64 = string.Empty;

        var plantilla = obtenerPlantillaAcuseOficioIdActuarioyPorFecha("PlantillaAcuseOficio.docx");

        Document docWord = null;

        var patron = "";

        using (MemoryStream stream = new MemoryStream(plantilla))
        {

            docWord = new Document(stream);

            Table table = (Table)docWord.GetChildNodes(NodeType.Table, true).Last();
            Row clonedRow = (Row)table.LastRow.Clone(true);

            int i = 0;
            foreach (var autoridad in listaItems)
            {
                var valores = autoridad.ToDictionary();

                valores["Fecha"] = autoridad.Fecha.ToString("dd MMMM yyyy");

                if (i > 0)
                {
                    Row emptyRow = (Row)clonedRow.Clone(true);
                    table.Rows.Add(emptyRow);
                }
                foreach (KeyValuePair<string, string> entry in valores)
                {
                    patron = "@" + entry.Key;

                    if (!String.IsNullOrEmpty(entry.Value))
                    {
                        docWord.Range.Replace(new Regex(patron), entry.Value);
                    }
                }
                docWord = AgregaCodigoQR(docWord, autoridad.Expediente ?? "", autoridad.Folio);
                i++;
            }

            using (MemoryStream ms = new MemoryStream())
            {
                docWord.Save(ms, Aspose.Words.SaveFormat.Pdf);
                ms.Position = 0L;
                byte[] PDFBytes = new byte[ms.Length];
                ms.Read(PDFBytes, 0, PDFBytes.Length);
                ms.Close();
                base64 = Convert.ToBase64String(PDFBytes);
            }

            return base64;
        }
    }

    public string ObtenDocumentoPorFecha(List<ConsultaOficioActuario> listaItems)
    {
        var listaPorExpediente = listaItems.GroupBy(x => x.Expediente).ToList();

        string base64 = string.Empty;

        var nombrePlantilla = listaItems.FirstOrDefault().CatOrganismoIdUnc == 0 ? "PlantillaAcuseOficio.docx" : "PlantillaAcuseOficioUNC.docx";

        var plantilla = obtenerPlantillaAcuseOficioIdActuarioyPorFecha(nombrePlantilla);

        Document docWord = null;

        var patron = "";

        using (MemoryStream stream = new MemoryStream(plantilla))
        {

            docWord = new Document(stream);

            var DocumentEmpty = docWord.Clone();

            var table = (Table)docWord.GetChildNodes(NodeType.Table, true).Last();

            Table emptyTable = (Table)table.Clone(true);

            Row clonedRow = (Row)table.LastRow.Clone(true);

            int i = 0;
            int x = 0;


            foreach (var autoridad in listaPorExpediente)
            {
                Table clone = (Table)emptyTable.Clone(true);

                if (x > 0)
                {
                    i = 0;

                    var newPage = DocumentEmpty.Clone();

                    docWord.AppendDocument(newPage, ImportFormatMode.KeepSourceFormatting);

                    clone = (Table)docWord.GetChildNodes(NodeType.Table, true).Last();

                }


                foreach (var item in autoridad)
                {
                    var valores = item.ToDictionary();

                    valores["Fecha"] = item.Fecha.ToString("dd MMMM yyyy");

                    if (i > 0)
                    {
                        if (x > 0)
                        {
                            Row emptyRow = (Row)clonedRow.Clone(true);
                            clone.Rows.Add(emptyRow);
                        }
                        else
                        {
                            Row emptyRow = (Row)clonedRow.Clone(true);
                            table.Rows.Add(emptyRow);
                        }
                    }
                    foreach (KeyValuePair<string, string> entry in valores)
                    {
                        patron = "@" + entry.Key;

                        if (!String.IsNullOrEmpty(entry.Value))
                        {
                            docWord.Range.Replace(new Regex(patron), entry.Value);
                        }
                    }

                    docWord = AgregaCodigoQR(docWord, item.Expediente ?? "", item.Folio);
                    
                    if (nombrePlantilla.Contains("UNC"))
                    {
                        docWord = AgregaCodigoQR(docWord, item.Expediente ?? "", item.Folio, true, item.CatOrganismoIdUnc, item.AnexoTipoId.ToString(), item.AsuntoNeunId);
                    }

                    i++;
                }
                x++;
            }


            using (MemoryStream ms = new MemoryStream())
            {
                docWord.Save(ms, Aspose.Words.SaveFormat.Pdf);
                ms.Position = 0L;
                byte[] PDFBytes = new byte[ms.Length];
                ms.Read(PDFBytes, 0, PDFBytes.Length);
                ms.Close();
                base64 = Convert.ToBase64String(PDFBytes);
            }

            return base64;
        }
    }


    private byte[] obtenerPlantillaAcuseOficioIdActuarioyPorFecha(string plantilla)
    {
        return _documentoBlob.ObtenerBlobDocumento(plantilla, _configuration["SISE3:BackEnd:OficiosPlantillasContenedor"],
            _configuration["SISE3:BackEnd:SMTPTemplatesUrl"]).ConfigureAwait(false).GetAwaiter().GetResult();
    }

    private Document AgregaCodigoQR(Document documento, string expediente, int folio, bool unc = false, int catOrganismoId = 0, string tipoAnexo = "", long asuntoNeunId = 0)
    {
        var base64Qr = unc ? GeneraQrUnc(catOrganismoId.ToString(), tipoAnexo, folio.ToString(), asuntoNeunId.ToString()) : GeneraQr(expediente, folio.ToString());

        NodeCollection shapes = documento.GetChildNodes(NodeType.Shape, true);

        foreach (Shape nshape in shapes)
        {
            if (nshape.AlternativeText.Contains("QR") || nshape.AlternativeText == "")
            {
                nshape.ImageData.SetImage(base64Qr);
                nshape.AlternativeText = folio.ToString();
                break;
            }
        }

        return documento;
    }


    private Stream GeneraQr(string expediente, string folio)
    {
        var key = "qr1";
        var value = "{\"E\":{\"NE\":" + expediente
                + ",\"O\":{\"F\":" + folio + ",\"A\":" + DateTime.Today.Year.ToString() + "}}";

        var response = InsertQRCodeInWordDocument(key, value, 10);

        return response;
    }


    private Stream GeneraQrUnc(string organismoId, string tipoAnexo, string oficio, string asuntoNeunId)
    {
        var key = "qr2";
        var value = $"{organismoId}/{tipoAnexo}/" +
                    $"{DateTime.Today.Year}/" + $"{oficio}/{asuntoNeunId}";

        var response = InsertQRCodeInWordDocument(key, value, 10);

        return response;
    }


    private Stream InsertQRCodeInWordDocument(string qrToRemplace, string qrCodeText, int pixelsPerQRModule)
    {
        var uri = _configuration["SISE3:BackEnd:SMTPTemplatesUrl"];
        var contenedorPlantillas = _configuration["SISE3:BackEnd:OficiosPlantillasRecursosContenedor"];
        var identificadorPlantillaSISE = qrToRemplace.Trim() + ".png";
        var logoPng = _documentoBlob.ObtenerBlobDocumento(identificadorPlantillaSISE, contenedorPlantillas, uri).ConfigureAwait(false).GetAwaiter().GetResult();

        var byteQR = _generadorQR.GenerarQRWithLogo(qrCodeText, pixelsPerQRModule, logoPng);
        var streamImageQR = UtilsImages.ImageFromRawBgraArray(byteQR);

        return streamImageQR;
    }
}
