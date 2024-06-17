using System.Text.RegularExpressions;
using Aspose.Words;
using Aspose.Words.Replacing;
using Aspose.Words.Saving;
using CJF.Sgcpj.Judicatura.Application.Utils;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;

namespace CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
public class WordContenido : IWordContenido
{
    private readonly ISanitizerService _sanitizerService;

    public WordContenido(ISanitizerService sanitizerService)
    {
        _sanitizerService = sanitizerService;
    }


    public string ReadDocumentWord(byte[] data)
    {
        if (data == null)
            throw new ArgumentNullException();

        var streamDocument = Functions.MallocFromArrayBytes(data);

        Document doc = new Document(streamDocument);

        HtmlSaveOptions options = new HtmlSaveOptions(SaveFormat.Html);
        options.ExportImagesAsBase64 = true;

        MemoryStream streamModify = new MemoryStream();
        doc.Save(streamModify, options);
        streamModify.Position = 0;

        StreamReader stream = new StreamReader(streamModify);

        return stream.ReadToEnd();
    }
    public byte[] ReplaceHtmlInWordBookmark(byte[] data, string html)
    {
        if (data == null)
            throw new ArgumentNullException();

        var streamDocument = Functions.MallocFromArrayBytes(data);

        Document doc = new Document(streamDocument);
        DocumentBuilder builder = new DocumentBuilder(doc);

        builder.MoveToDocumentEnd();
        builder.MoveToBookmark("Acuerdo");
        builder.InsertHtml(html);

        MemoryStream stream = new MemoryStream();
        doc.Save(stream, SaveFormat.Docx);
        stream.Seek(0, SeekOrigin.Begin);
        return Functions.ConvertStreamToByteArray(stream);
    }
    public byte[] ReplaceHtmlInWord(byte[] data, string html)
    {
        if (data == null)
            throw new ArgumentNullException();

        html = InsertarComillasHtml(html);
        html = RemoveImageHtml(html);
        var htmlSanitized = _sanitizerService.SanitizeHtml(html);

        htmlSanitized = RemoveSups(htmlSanitized);

        RelationBlockQuote obj = new RelationBlockQuote()
        {
            BlockQuoteString = Regex.Matches(htmlSanitized, "<blockquote[^>]*>(.*?)</blockquote>", RegexOptions.Singleline)
                                       .Cast<Match>()
                                       .Select(match => match.Groups[1].Value.Trim())
                                       .ToList(),
            ParagraphString = RemoveBlockquote(htmlSanitized)
        };

        var streamDocument = Functions.MallocFromArrayBytes(data);

        Document doc = new Document(streamDocument);
        DocumentBuilder builder = new DocumentBuilder(doc);

        NodeCollection nodos = doc.GetChildNodes(NodeType.Run, true);
        foreach (var run in nodos.OfType<Run>().Where(run => run.Text.Equals("Acuerdo_Remplazar")))
        {
            Paragraph paragraph = (Paragraph)run.GetAncestor(NodeType.Paragraph);
            run.Text = run.Text.Replace("Acuerdo_Remplazar", "");
            paragraph.ParagraphFormat.LeftIndent = ConvertUtil.InchToPoint(1.25984);
            paragraph.ParagraphFormat.RightIndent = ConvertUtil.InchToPoint(0.0984252);
            builder.MoveTo(paragraph);
            builder.InsertHtml(obj.ParagraphString, HtmlInsertOptions.UseBuilderFormatting | HtmlInsertOptions.RemoveLastEmptyParagraph);
        }

        if (obj.BlockQuoteString.Count > 0)
        {
            for (int footnoteIndex = 1; footnoteIndex <= obj.BlockQuoteString.Count; footnoteIndex++)
            {
                FindReplaceOptions opt = new FindReplaceOptions();
                opt.ReplacingCallback = new AddInserterNote(StripHtmlTags(obj.BlockQuoteString[footnoteIndex - 1]));
                doc.Range.Replace($"[sup:{footnoteIndex}]", string.Empty, opt);
            }

            for (int footnoteIndex = 1; footnoteIndex <= obj.BlockQuoteString.Count; footnoteIndex++)
            {
                doc.Range.Replace($"[sup:{footnoteIndex}]", "", new FindReplaceOptions());
            }
        }

        MemoryStream stream = new MemoryStream();
        doc.Save(stream, SaveFormat.Docx);
        stream.Seek(0, SeekOrigin.Begin);
        return Functions.ConvertStreamToByteArray(stream);
    }

    public byte[] SaveWordtoPdf(byte[] data)
    {
        if (data == null)
            throw new ArgumentNullException();

        var streamDocument = Functions.MallocFromArrayBytes(data);

        Document doc = new Document(streamDocument);

        MemoryStream stream = new MemoryStream();
        doc.Save(stream, SaveFormat.Pdf);
        stream.Seek(0, SeekOrigin.Begin);
        return Functions.ConvertStreamToByteArray(stream);
    }



    #region "Private methods"

    private string RemoveBlockquote(string text)
    {
        string pattern = "<blockquote[^>]*>.*?</blockquote>";
        return Regex.Replace(text, pattern, String.Empty, RegexOptions.Singleline).Trim();
    }

    private string RemoveSups(string text)
    {
        text = Regex.Replace(text, @"<sup[^>]*><br\s*/?></sup>", string.Empty, RegexOptions.Singleline).Trim();
        text = Regex.Replace(text, @"<sup[^>]*>(.*?)</sup>", match => $"[sup:{match.Groups[1].Value}]").Trim();
        return text;
    }

    private string StripHtmlTags(string input)
    {
        input = Regex.Replace(input, @"<sup><br></sup>", String.Empty, RegexOptions.Singleline).Trim();
        input = Regex.Replace(input, "<sup[^>]*>.*?</sup>", String.Empty, RegexOptions.Singleline).Trim();
        input = Regex.Replace(input, "&#\\d+;", String.Empty, RegexOptions.Singleline).Trim();
        input = Regex.Replace(input, @"\[sup:(\d+)\]", String.Empty, RegexOptions.Singleline).Trim();
        input = Regex.Replace(input, "<.*?>", String.Empty, RegexOptions.Singleline).Trim();

        return input;
    }
    private string InsertarComillasHtml(string html)
    {
        int contador = 0;

        MatchCollection matches = Regex.Matches(html, @"<p\b[^>]*>(.*?)<\/p>", RegexOptions.Singleline);
        foreach (Match match in matches)
        {
            string contenidoParrafo = Regex.Replace(match.Value, @"<[^>]*>", "");

            if (contenidoParrafo != "&#xa0;" && contador == 0)
            {
                string htmlTemporal = Regex.Replace(match.Value, @"<p\b[^>]*>", matchr =>
                {
                    if (matchr.Index == match.Value.IndexOf(matchr.Value) && contador == 0)
                    {
                        contador++;
                        return matchr.Value.Insert(matchr.Value.IndexOf('>') + 1, "\"");
                    }
                    return matchr.Value;
                }, RegexOptions.Singleline);
                html = html.Replace(match.Value, htmlTemporal);

            }
            if (matches.LastOrDefault()?.Index == match.Index || matches.LastOrDefault()?.Index == 0)
            {
                contador = 0;
                if (string.IsNullOrWhiteSpace(contenidoParrafo))
                {
                    html = Regex.Replace(html, @"<img[^>]*>", match =>
                    {
                        if (match.Index >= matches.LastOrDefault()?.Index && contador == 0)
                        {
                            contador++;
                            return match.Value.Insert(match.Value.IndexOf('>') + 1, "\"");
                        }
                        return match.Value;
                    }, RegexOptions.Singleline);
                }
                if (Regex.IsMatch(html, @"<p\b[^>]*>(?:(?!</?p\b).)*?<img\b[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Singleline) && contador == 0)
                {
                    html = Regex.Replace(html, @"<\/p>|<br\s*\/?>", match =>
                    {
                        if (match.Index >= matches.LastOrDefault()?.Index && contador == 0)
                        {
                            if (match.Index == html.LastIndexOf(match.Value))
                            {
                                contador++;
                                return match.Value.Insert(0, "\"");
                            }
                            return match.Value;
                        }
                        return match.Value;
                    }, RegexOptions.Singleline);
                }
                if (!string.IsNullOrWhiteSpace(html) && contador == 0)
                    AgregarComillasFinal(ref html, contador, matches);
            }
        }
        return html;
    }

    private static void AgregarComillasFinal(ref string html, int contador, MatchCollection matches)
    {
        html = Regex.Replace(html, @"<\/p>|<br\s*\/?>", match =>
        {
            if (match.Index >= matches.LastOrDefault()?.Index && contador == 0)
            {
                contador++;
                return match.Value.Insert(0, "\"");
            }
            return match.Value;
        }, RegexOptions.Singleline);
    }

    private string RemoveImageHtml(string html)
    {

        html = Regex.Replace(html, @"<span\s+style\s*=\s*""[^""]*\bz-index\s*:\s*-1[^""]*""[^>]*>(.*?)<img[^>]*>.*?</span>"
                                            , m => m.Groups[1].Value, RegexOptions.IgnoreCase | RegexOptions.Singleline);

        return html;
    }

    #endregion
}
