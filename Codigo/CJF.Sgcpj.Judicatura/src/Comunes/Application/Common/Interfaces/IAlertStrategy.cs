namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;

public interface IAlertStrategy
{
    Task SendAlert(string title, string message, IEnumerable<string> to);
}
