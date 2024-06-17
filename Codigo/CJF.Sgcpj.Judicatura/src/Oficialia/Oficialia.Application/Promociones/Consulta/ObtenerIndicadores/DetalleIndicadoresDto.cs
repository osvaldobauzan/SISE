using CJF.Sgcpj.Judicatura.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerNumeroPromocion;
public class DetalleIndicadoresDto : IMapFrom<DetalleIndicadores>
{
        public long EmpleadoId { get; set; }
        public string? NombreOficial { get; set; }
        public string? UserName { get; set; }
        public int TotalPromociones { get; set; }
        public int PromocionesTurnadas { get; set; }
        public int TotalPromocionesAnoActual { get; set; }
        public double PromedioPromocionesTurnadasPorDia { get; set; }
        public double TiempoPromedioMins { get; set; }

}
