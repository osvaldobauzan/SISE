using CJF.Sgcpj.Judicatura.Actuaria.Domain.Entities;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DiferenciasNotificaciones;
public class ObtenerDiferenciasResponseDto
{
    public List<Domain.Entities.DiferenciasTiempos> Diferencias { get; set; }
}
