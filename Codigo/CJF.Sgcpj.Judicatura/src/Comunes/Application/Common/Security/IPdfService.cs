namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Security;

public interface IPdfService {
    byte[] AddSings(byte[] data, string[] firmas);

}