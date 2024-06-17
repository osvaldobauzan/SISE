using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Application.Catalogos.Contenido.Consulta;
using CJF.Sgcpj.Judicatura.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Application.Cuadernos.Consulta;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.InsertarPromocion;
public class InsertarPromocionDto:IMapFrom<Common.Models.InsertarPromocion>
{
    public long AsuntoNeunId { get; set; }
    public int TipoCuaderno { get; set; }
    public string FechaPresentacion { get; set; }
    public string HoraPresentacion { get; set; }
   
    public int? ClasePromocion { get; set; }
    public int? ClasePromovente { get; set; }
    public int TipoPromovente { get; set; }
    public int TipoContenido { get; set; }
    public int NumeroCopias { get; set; }
    public int NumeroAnexo { get; set; }
    public int Secretario { get; set; }   
  
    public int RegistroEmpleadoId { get; set; }
    public string Observaciones { get; set; }
    public string IpUsuario { get; set; }
    public int? OrigenPromocion { get; set; }
    public int? NumeroRegistro { get; set; }
    public int? NumeroOrden { get; set; }
    public int? Fojas { get; set; }
    public bool ArchivoAVincular { get; set; }
    public string TipoAsunto { get; set; }
    public string TipoProcedimiento { get; set; }
    public string Mesa { get; set; }
    public string NumeroExpediente { get; set; }
    public int? Origen { get; set; }
    public long? Folio { get; set; }
    public bool ConExpedienteElectronico { get; set; }
}
