using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Models;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.Audiencia;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.Sentencia;
using ExpedienteElectronico.Application.Common.Models;
using ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.DatosGenerales;
using ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.InformacionParte;

namespace CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Repositories;
public interface IExpedienteElectronicoRepository
{
    Task<List<DetalleExpedienteElectronico>> ObtenerExpedienteElectronico(ExpedienteElectronicoFiltro param);
    Task<List<FichaTecnicaExpedienteElectronico>> ObtenerFichaTecnicaExpediente(FichaTecnicaExpedienteElectronicoFiltro param);
    Task<Int64> InsertarPersonaAsunto(PersonaAsuntoInsert param);
    Task<bool> EliminarPersonaAsunto(PersonaAsuntoDelete param);
    Task<bool> ActualizarPersonaAsunto(PersonaAsuntoUpdate param);
    Task<PersonaAsuntoDTO> ObtenerPersonaAsunto(PersonaAsuntoFiltro param);
    Task<EstadoSentencia> ObtenerEstadoSentencia(EstadoSentenciaConsulta request);
    Task<DetalleAudiencia> ObtenerDetalleAudiencia(AudienciaConsulta request);
    Task<List<InformacionParteM>> ObtenerInformacionParte(InformacionParteConsulta request);
    Task<DatosGeneralesM?> ObtenerDatosGenerales(DatosGeneralesConsulta request);
}
