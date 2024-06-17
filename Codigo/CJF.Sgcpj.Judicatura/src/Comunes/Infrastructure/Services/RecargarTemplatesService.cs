using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;

namespace CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
public class RecargarTemplatesService : IRecargarTemplatesService
{
    public bool EsPrimeraVez { get; set; } = true;
}
