namespace CJF.Sgcpj.Judicatura.Application.Common.Interfaces;

public interface IAlertStrategy
{
    Task SendAlert(string title, string message, IEnumerable<string> to);
}
