using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DetalleNotificaciones;

public class DatosAsuntoDto : IMapFrom<DatosAsunto>
{
    public  string NombreArchivo { get; set; }
    public  int Transcurrido { get; set; }
    public  string No_Exp { get; set; }
    public  string TipoAsuntoDescripcion { get; set; }
    public  string TipoCuaderno { get; set; }
    public  string TipoProcedimiento { get; set; }

}