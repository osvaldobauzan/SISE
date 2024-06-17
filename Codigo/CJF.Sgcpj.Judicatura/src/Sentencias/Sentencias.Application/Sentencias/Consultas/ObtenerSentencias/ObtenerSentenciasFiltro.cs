using MediatR;

namespace CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Consultas.ObtenerSentencias;

public class ObtenerSentenciasFiltro : IRequest<TableroSentenciasDto>
{
    public string Fecha { get; init; }

    public string FechaFin { get; init; }

    public int CatOrganismoId { get; set; }
}
