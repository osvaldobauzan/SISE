extern alias BC;
using System.Security.Cryptography.Pkcs;
using System.Text.RegularExpressions;
using AngleSharp.Common;
using Aspose.Words;
using CJF.Firma.Util.Model;
using CJF.Firma.Util.Ocsp;
using CJF.Firma.Util.Sign;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
using Documentos.Application.Common.Model.GenerarEvidenciaFirma;
using Microsoft.Extensions.Configuration;
using Aspose.Words;
using Aspose.Pdf.Operators;
using System.Security.Cryptography.Xml;
using FastExpressionCompiler;
using Org.BouncyCastle.Ocsp;
using BC::Org.BouncyCastle.X509;
using Aspose.Words.MailMerging;
using Org.BouncyCastle.Cms;
using SkiaSharp;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Tsp;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using System.Security.Policy;
using Org.BouncyCastle.Math;
using CJF.Firma.Util.Exceptions;
using System.Net;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Collections;
using static IdentityModel.OidcConstants;
using DocumentFormat.OpenXml.EMMA;
using CJF.Firma.Util.acrs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using Aspose.Words.Shaping;

namespace Documentos.Application.FirmadorDocumentos.Comandos.GenerarEvidenciaFirma;

public class HojaFirmasService : IHojaFirmasService
{
    private readonly IConfiguration _configuration;
    private readonly IDocumentoBlob _documentoBlob;

    public HojaFirmasService(IConfiguration configuration, IDocumentoBlob documentoBlob)
    {
        _configuration = configuration;
        _documentoBlob = documentoBlob;
    }

    public GenerarEvidienciaFirmaDTO GenerarHojaFirmas(GenerarEvidienciaFirmaFiltro request)
    {
        GenerarEvidienciaFirmaDTO evidencia = new GenerarEvidienciaFirmaDTO();
        var firmantes = ObtenerCertificados(request.p7m, request.ArchivoFirmado, request.GuidDocumento);
        var documento = GeneraDocuento(firmantes);
        evidencia.Contenido = documento;
        evidencia.PrimerFirmante = firmantes.First().Nombre;
        evidencia.Hash = firmantes.First().NoSerie;
        evidencia.Fecha = firmantes.First().FechaFirma;
        return evidencia;
    }

    private List<DatosFirmantesDto> ObtenerCertificados(string pkcs7, string archivoFirmado, string respuestaTPS)
    {
        byte[] archivoPkcs7 = Convert.FromBase64String(pkcs7);
        var firmantes = new List<DatosFirmantesDto>();
        var cmsMs = new SignedCms();
        cmsMs.Decode(archivoPkcs7);
        var lstTSP = GetTSP(archivoPkcs7);

        foreach (var signer in cmsMs.SignerInfos)
        {
            DatosCertificado dtoCert = new FirmaUtil().GetDatos(signer.Certificate);

            var resTsp = lstTSP.Where(k => k.SerialNumber == dtoCert.Certificate.SerialNumber.ToString()).FirstOrDefault();
            var hash = resTsp.Hash;
            var fechaTSP = resTsp.TimeStamp;
            var tsp = signer.UnsignedAttributes[0].Values[0].RawData;
            var bouncy = new BC.Org.BouncyCastle.X509.X509CertificateParser();
            var bcert = bouncy.ReadCertificate(tsp);
            var cerTps = new System.Security.Cryptography.X509Certificates.X509Certificate2(bcert.GetEncoded());
            var dtoTps = new FirmaUtil().GetDatos(cerTps);
            var cmsTsp = new SignedCms();
            cmsTsp.Decode(tsp);
            var signerTsp = cmsTsp.SignerInfos[0];

            /*************************************************/
            //System.Security.Cryptography.X509Certificates.X509Certificate2 objCert = signer.Certificate;
            //System.Security.Cryptography.X509Certificates.X509Certificate2 x509Certificate = signer.Certificate;
            //var cert = DotNetUtilities.FromX509Certificate(x509Certificate);


            //var ocspUrl = "";
            //try
            //{

            //    BigInteger serialNumber = cert.SerialNumber;
            //    List<string> urls = new List<string>();
            //    byte[] str = new byte[16];
            //    Hashtable hashtable = new Hashtable();
            //    X509Extension value =
            //        new X509Extension(critical: false, new DerOctetString(str));
            //    hashtable.Add(BC.Org.BouncyCastle.Asn1.Ocsp.OcspObjectIdentifiers.PkixOcspNonce, value);


            //    List<DerObjectIdentifier> ders = new List<DerObjectIdentifier>();
            //    List<X509Extension> xes = new List<X509Extension>();

            //    ders.Add(OcspObjectIdentifiers.PkixOcspNonce);
            //    xes.Add(value);
            //    var GetExtentions = new X509Extensions(ders, xes);

            //    OcspReqGenerator ocspReqGenerator = new OcspReqGenerator();
            //    CertificateID certId = new CertificateID("1.3.14.3.2.26", dtoCert.Chain.Last(), serialNumber);
            //    ocspReqGenerator.AddRequest(certId);
            //    ocspReqGenerator.SetRequestExtensions(GetExtentions);
            //    OcspReq ocspReq = ocspReqGenerator.Generate();
            //    byte[] ocspPackage = ocspReq.GetEncoded();

            //    switch (dtoCert.Issuer)
            //    {
            //        case Issuer.CJF:
            //            urls.Add("http://www.uncocefi.cjf.gob.mx:1320/OCSP");
            //            urls.Add("http://firel.uncocefi.cjf.gob.mx:1320/OCSP");                        
            //            break;
            //        case Issuer.SCJN:
            //            urls.Add("http://ocsp.scjn.gob.mx:8085/");
            //            urls.Add("http://ocsp.scjn.gob.mx:8086/");
            //            break;
            //        case Issuer.TEPJF:
            //            urls.Add("http://uce.te.gob.mx:1350/OCSPFIREL");
            //            urls.Add("http://uce.te.gob.mx:1350/OCSP");
            //            break;
            //        case Issuer.SAT:
            //            urls.Add("https://cfdi.sat.gob.mx/edofiel");
            //            break;
            //        default:
            //            throw new NotSupportedException("No se identifica el emisor del certificado");
            //    }
            //    ocspUrl = urls.First();
            //    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(ocspUrl);
            //    try
            //    {

            //        httpWebRequest.ContentType = "application/ocsp-request";
            //        httpWebRequest.Accept = "application/ocsp-response";
            //        httpWebRequest.Method = "POST";
            //        httpWebRequest.ContentLength = ocspPackage.Length;
            //        httpWebRequest.KeepAlive = true;
            //        httpWebRequest.UserAgent = "curl/7.46.0";
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception("falla en la configuraciuón: " + ocspUrl + ex.Message);
            //    }
            //    try
            //    {
            //        using (Stream stream = httpWebRequest.GetRequestStream())
            //        {
            //            stream.Write(ocspPackage, 0, ocspPackage.Length);
            //            stream.Flush();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception("falla en el GetRequestStream: " + ocspUrl + ex.Message);
            //    }
            //    try
            //    {

            //        HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            //        if (httpWebResponse.StatusCode != HttpStatusCode.OK)
            //        {
            //            throw new CertificateException("El servicio respondió con estatus " + ocspUrl + httpWebResponse.StatusDescription);
            //        }

            //        using MemoryStream memoryStream = new MemoryStream();

            //        using (Stream stream2 = httpWebResponse.GetResponseStream())
            //        {
            //            try
            //            {
            //                stream2.CopyTo(memoryStream);

            //            }
            //            catch ( Exception ex)
            //            {
            //                throw new Exception("falla en el GetResponseStream 2: " + ocspUrl + ex.Message);
            //            }
            //        }

            //        memoryStream.ToArray();
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception("falla en el GetResponse: " + ocspUrl + ex.Message);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("falla en el GetResponse: " + ocspUrl + ex.Message);
            //}

            /************************************************/

            //var ocspValidator = new CJF.Firma.Util.Ocsp.OcspValidator(ref dtoCert);
            //var respOcsp = ocspValidator.GetResponseOcsp();
            //OcspResp ocspResponse = new OcspResp(respOcsp);
            //BasicOcspResp basicOcspResponse = (BasicOcspResp)ocspResponse.GetResponseObject();
            //var certsOcsp = basicOcspResponse.GetCerts()[0];
            //var respondedorOCSP = GetCommonName(certsOcsp.SubjectDN.ToString());
            //var serieOCSP = ToHexString(certsOcsp.CertificateStructure.SerialNumber.Value.ToByteArray());

            var respondedorOCSP = string.Empty;
            var serieOCSP = string.Empty;
            switch (dtoCert.Issuer)
            {
                case Issuer.CJF:
                    respondedorOCSP = "OCSP ACI del Consejo de la Judicatura Federal";
                    serieOCSP = "70.6A.66.20.63.6A.66.03";
                    break;
                case Issuer.SCJN:
                    respondedorOCSP = "OCSP ACI del Consejo de la Judicatura Federal";
                    serieOCSP = "70.6A.66.20.63.6A.66.03";
                    break;
                case Issuer.TEPJF:
                    respondedorOCSP = "OCSP de la Unidad de Certificacion Electronica del TEPJF - PJF";
                    serieOCSP = "30.30.30.32.33.30";
                    break;
                case Issuer.SAT:
                    respondedorOCSP = "Servicio OCSP SAT";
                    serieOCSP = "30.30.30.30.31.30.38.38.38.38.38.38.30.30.30.30.30.30.33.39";
                    break;
                default:
                    respondedorOCSP = string.Empty;
                    serieOCSP = string.Empty;
                    break;
            }

            var fechafirma = string.Empty;
            foreach (var atributosDeFirma in signer.SignedAttributes)
            {
                if (atributosDeFirma.Oid.Value == "1.2.840.113549.1.9.5")
                {
                    fechafirma = GetDateUTCCDMX(((System.Security.Cryptography.Pkcs.Pkcs9SigningTime)atributosDeFirma.Values[0]).SigningTime);
                    break;
                }
            }
            var CheckSumContent = ToHexString(signer.GetSignature());

            firmantes.Add(new DatosFirmantesDto()
            {
                ArchivoFirmado = archivoFirmado,
                NoSerie = GetFormatNoSerie(dtoCert.Certificate.SerialNumber.ToString(16)),
                Nombre = dtoCert.Certificate.SubjectDN.GetValueList()[0].ToString(),
                CadenaFirma = GetSing(CheckSumContent),
                Algoritmo = signer.SignatureAlgorithm.FriendlyName.ToUpper() + " - " + signer.DigestAlgorithm.FriendlyName.ToUpper(),
                FechaFirma = fechafirma,
                FechaOCSP = fechafirma,
                EmisorOCSP = GetCommonName(signer.Certificate.Issuer),
                FechaTSP = fechaTSP,
                EmisorTSP = GetCommonName(signerTsp.Certificate.Subject),
                EmisorCertificadoTPS = GetCommonName(signerTsp.Certificate.Issuer),
                AutoridadCertificadora = GetCommonName(signer.Certificate.Issuer),
                NumeroFirmantes = cmsMs.SignerInfos.Count.ToString(),
                Estampilla = hash,
                RespuestaTPS = respuestaTPS,
                RespondedorOCSP = respondedorOCSP,
                SerieOCSP = serieOCSP.Replace('-', '.'),
            });
        }
        firmantes = firmantes.OrderBy(s => s.FechaFirma).ToList();
        return firmantes;
    }

    private string GeneraDocuento(List<DatosFirmantesDto> firmantes)
    {
        string base64 = string.Empty;
        AsposeLicense licencia = new AsposeLicense(_configuration);
        Aspose.Words.License wordsLicencia = new Aspose.Words.License();
        wordsLicencia.SetLicense(licencia.GetLicense());


        var uri = _configuration["SISE3:BackEnd:SMTPTemplatesUrl"];
        var contenedorPlantillas = _configuration["SISE3:BackEnd:FirmadorPlantillasContenedor"];
        var identificadorPlantilla = "PlantillaEvidencia.docx";
        var plantilla = _documentoBlob.ObtenerBlobDocumento(identificadorPlantilla, contenedorPlantillas, uri).ConfigureAwait(false).GetAwaiter().GetResult();

        Document docWord = null;

        var patron = "";
        using (MemoryStream stream = new MemoryStream(plantilla))
        {
            docWord = new Document(stream);
            var table = docWord.GetChildNodes(NodeType.Table, true).First().Clone(true);
            int i = 0;
            foreach (var firmante in firmantes)
            {
                var valores = firmante.ToDictionary();
                if (i > 0)
                {
                    Section section = new Section(docWord);
                    docWord.AppendChild(section);
                    section.PageSetup.SectionStart = SectionStart.NewPage;
                    section.PageSetup.PaperSize = PaperSize.Letter;
                    Body body = new Body(docWord);
                    section.AppendChild(body);
                    body.AppendChild(table);
                }
                foreach (KeyValuePair<string, string> entry in valores)
                {
                    patron = "@" + entry.Key;
                    if (!String.IsNullOrEmpty(entry.Value))
                    {
                        if (entry.Value.Contains("@@salto@@"))
                        {
                            docWord.Range.Replace(new Regex(patron), entry.Value.Replace("@@salto@@", ControlChar.LineBreak));

                        }
                        else
                        {
                            docWord.Range.Replace(new Regex(patron), entry.Value);
                        }
                    }
                }
                i++;
            }
            byte[] documento;
            using (MemoryStream ms = new MemoryStream())
            {
                docWord.Save(ms, Aspose.Words.SaveFormat.Pdf);
                ms.Position = 0L;
                byte[] PDFBytes = new byte[ms.Length];
                ms.Read(PDFBytes, 0, PDFBytes.Length);
                ms.Close();
                base64 = Convert.ToBase64String(PDFBytes);
            }
        }

        return base64;
    }

    private string ToHexString(byte[] byteArray)
    {
        var hexEncodedArray = BitConverter.ToString(byteArray, 0, byteArray.Length);
        return hexEncodedArray;
    }
    private string GetDateUTCCDMX(DateTime utcDateTime)
    {
        string cdmxTimeZoneKey = "America/Mexico_City";
        TimeZoneInfo nzTimeZone = TimeZoneInfo.FindSystemTimeZoneById(cdmxTimeZoneKey);
        DateTime nzDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, nzTimeZone);

        return utcDateTime.ToString("dd/MM/yyyy HH:mm:ss") + " - " + nzDateTime.ToString("dd/MM/yyyy HH:mm:ss");

    }
    private string GetFormatNoSerie(string sing)
    {
        sing = sing.Replace("-", "");
        string firma = string.Empty;
        var espacio = 1;
        for (var caracter = 0; caracter < sing.Length; caracter++)
        {
            if (espacio == 2)
            {
                firma = firma + sing.Substring(caracter, 1);
                firma = firma + ".";
                espacio = 1;
            }
            else
            {
                firma = firma + sing.Substring(caracter, 1);
                espacio++;
            }
        }
        firma = firma.TrimEnd('.');
        return firma.ToLower();
    }
    private string GetSing(string sing)
    {
        sing = sing.Replace("-", "");
        string firma = string.Empty;
        var retorno = 0;
        var espacio = 1;
        for (var caracter = 0; caracter < sing.Length; caracter++)
        {
            if (retorno == 32)
            {
                firma = firma + "@@salto@@";
                retorno = 0;
            }

            if (espacio == 2)
            {
                firma = firma + sing.Substring(caracter, 1);
                firma = firma + " ";
                espacio = 1;
            }
            else
            {
                firma = firma + sing.Substring(caracter, 1);
                espacio++;
            }

            retorno++;
        }
        return firma.ToLower();
    }
    private string GetCommonName(string subject)
    {
        string[] tempSubject;
        string[] tempCN;

        tempSubject = subject.Replace("CN=", "!").Split('!');
        tempCN = tempSubject[1].Split(',');

        return tempCN[0];
    }
    private List<DatosTSA> GetTSP(byte[] archivoPkcs7)
    {
        List<DatosTSA> listTSP = new List<DatosTSA>();
        BC.Org.BouncyCastle.Cms.CmsSignedDataParser cmsSignedDataParser = new BC.Org.BouncyCastle.Cms.CmsSignedDataParser(archivoPkcs7);
        cmsSignedDataParser.GetSignedContent().Drain();
        BC.Org.BouncyCastle.Cms.SignerInformationStore signerInfos = cmsSignedDataParser.GetSignerInfos();

        foreach (BC.Org.BouncyCastle.Cms.SignerInformation signer2 in signerInfos.GetSigners())
        {
            if (signer2.UnsignedAttributes != null)
            {
                BC.Org.BouncyCastle.Asn1.Cms.Attribute attribute2 = signer2.UnsignedAttributes[new BC.Org.BouncyCastle.Asn1.DerObjectIdentifier("1.2.840.113549.1.9.16.2.14")];
                if (attribute2 != null)
                {
                    BC.Org.BouncyCastle.Asn1.Asn1Encodable asn1Encodable = attribute2.AttrValues[0];
                    BC.Org.BouncyCastle.Asn1.Cms.ContentInfo instance = BC.Org.BouncyCastle.Asn1.Cms.ContentInfo.GetInstance(asn1Encodable.ToAsn1Object());
                    BC.Org.BouncyCastle.Tsp.TimeStampToken timeStampToken = new BC.Org.BouncyCastle.Tsp.TimeStampToken(instance);
                    BC.Org.BouncyCastle.Tsp.TimeStampTokenInfo timeStampInfo = timeStampToken.TimeStampInfo;
                    BC.Org.BouncyCastle.Asn1.X509.X509Name x509Name = timeStampInfo.Tsa.Name as BC.Org.BouncyCastle.Asn1.X509.X509Name;
                    listTSP.Add(new DatosTSA()
                    {
                        Hash = Convert.ToBase64String(timeStampInfo.GetMessageImprintDigest()),
                        TimeStamp = GetDateUTCCDMX(timeStampInfo.GenTime),
                        SerialNumber = signer2.SignerID.SerialNumber.ToString()
                    });
                }
            }
        }
        return listTSP;
    }
}