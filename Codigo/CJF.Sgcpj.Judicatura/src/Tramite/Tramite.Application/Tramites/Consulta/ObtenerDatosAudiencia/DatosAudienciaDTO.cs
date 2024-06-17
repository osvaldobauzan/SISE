using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerDatosAudiencia;
public class DatosAudienciaDTO
{
    public List<AudienciasDTO> Audiencias { get; set; }
    public List<ResultadosAudienciaDTO> Resultados { get; set; }
    public List<TiposAsuntoDTO> TiposAsuntos { get; set; }
    public List<TiposAudienciaDTO> TiposAudiencias { get; set; }
}
