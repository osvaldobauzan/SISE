using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DetalleSintesis;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.EditarSintesis;
public class EditarSintesisDto : IMapFrom<EditarSintesisAcuerdo>
{
    public long AsuntoNeunId { get; set; }
    public short TipoCuaderno { get; set; }
    public string? NombreDocumento { get; set; }
    public string? ExtensionDocumento { get; set; }
    public short? Contenido { get; set; }
    public DateTime FechaAcuerdo { get; set; }
    public long UsuarioCaptura { get; set; }
    public int AsuntoDocumentoId { get; set; }
    public string? Sintesis { get; set; }
    public int? SintesisOrden { get; set; }
    public string? NombreArchivo { get; set; }
    public DateTime? FechaPublicacion { get; set; }
    public long? Titular { get; set; }
    public long? Parte1 { get; set; }
    public long? Parte2 { get; set; }
    public long? ActuarioId { get; set; }
}
