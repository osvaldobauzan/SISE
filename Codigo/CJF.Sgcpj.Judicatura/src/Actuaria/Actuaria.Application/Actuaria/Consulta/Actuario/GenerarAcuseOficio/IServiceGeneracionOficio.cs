
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models.Actuario;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Actuario.GenerarAcuseOficio;

public interface IServiceGeneracionOficio
{
    string ObtenDocumento(List<ConsultaOficioActuario> listaItems);
    string ObtenDocumentoPorFecha(List<ConsultaOficioActuario> listaItems);
}