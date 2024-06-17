namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Exceptions;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException(bool esPermisos=false) : base()
    {
        EsPermisos = esPermisos;
    }

    public bool EsPermisos { get; }
}
