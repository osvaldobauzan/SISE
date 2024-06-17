namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Exceptions;
public class XssDetectedException : Exception
{
    public XssDetectedException(string mensaje) : base(mensaje) { }
}
