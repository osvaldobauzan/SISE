using Aspose.Words.Replacing;
using Aspose.Words;

namespace CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
public class AddInserterNote : IReplacingCallback
{
    public string BlockQuoteString { get; set; }

    public AddInserterNote(string valor)
    {
        BlockQuoteString = valor;
    }
    ReplaceAction IReplacingCallback.Replacing(ReplacingArgs e)
    {
        Node currentNode = e.MatchNode;

        if (e.MatchOffset > 0)
            currentNode = SplitRun((Run)currentNode, e.MatchOffset);

        List<Run> runs = new List<Run>();

        int remainingLength = e.Match.Value.Length;
        while (
            remainingLength > 0 &&
            currentNode != null &&
            currentNode.GetText().Length <= remainingLength)
        {
            runs.Add((Run)currentNode);
            remainingLength -= currentNode.GetText().Length;

            do
            {
                currentNode = currentNode.NextSibling;
            } while (currentNode != null && currentNode.NodeType != NodeType.Run);
        }

        if (currentNode != null && remainingLength > 0)
        {
            SplitRun((Run)currentNode, remainingLength);
            runs.Add((Run)currentNode);
        }

        DocumentBuilder builder = new DocumentBuilder((Document)e.MatchNode.Document);
        builder.MoveTo(runs[0]);
        Aspose.Words.Notes.Footnote note = builder.InsertFootnote(Aspose.Words.Notes.FootnoteType.Footnote,
            this.BlockQuoteString.Trim());
        Run lastRun = runs[runs.Count - 1];
        lastRun.ParentNode.InsertAfter(note, lastRun);
        return ReplaceAction.Skip;
    }

    private static Run SplitRun(Run run, int position)
    {
        Run afterRun = (Run)run.Clone(true);
        run.ParentNode.InsertAfter(afterRun, run);
        afterRun.Text = run.Text.Substring(position);
        run.Text = run.Text.Substring((0), (0) + (position));
        return afterRun;
    }
}
