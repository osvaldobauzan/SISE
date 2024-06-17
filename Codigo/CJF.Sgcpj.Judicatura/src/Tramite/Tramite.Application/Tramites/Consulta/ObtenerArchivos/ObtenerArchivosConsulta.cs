using MediatR;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerArchivos;
public record ObtenerArchivosConsulta : IRequest<ArchivosPromocionDto>
{
    public long AsuntoNeunId { get; set; }
    public int YearPromocion { get; set; }
    public int NumeroOrden { get; set; }
    public int Origen { get; set; }
    public int TipoModulo { get; set; }
    public int NumeroRegistro { get; set; }
    public int AsuntoDocumentoId { get; set; }

}
