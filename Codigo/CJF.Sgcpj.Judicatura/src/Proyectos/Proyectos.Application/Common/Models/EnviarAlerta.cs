using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Models;

public class EnviarAlerta
{
    public List<Destinatario> Destinatarios { get; set; }

    public string Mensaje { get; set; }
}
