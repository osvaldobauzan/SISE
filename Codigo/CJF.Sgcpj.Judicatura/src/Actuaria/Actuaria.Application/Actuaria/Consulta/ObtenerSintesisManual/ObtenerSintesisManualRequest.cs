using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerSintesisManual;
public class ObtenerSintesisManualRequest: IRequest<List< ObtenerSintesisManualDTO>>
{
    public DateTime FechaPublicacion { get; set; }
}

