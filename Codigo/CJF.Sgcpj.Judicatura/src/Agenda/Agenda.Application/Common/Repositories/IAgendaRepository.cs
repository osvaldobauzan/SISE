using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Css.Values;
using CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Comandos.InsertarAgenda;
using CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Comandos.ModificarEstado;
using CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ObtenerAgendaFecha;
using CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ObtenerDetalleCaracter;
using CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ValidarAudiencia;
using CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ValidarNeun;

namespace CJF.Sgcpj.Judicatura.Agenda.Application.Common.Repositories;

public interface IAgendaRepository
{
    Task<ModificarEstadoDto> ModificarEstadoAudiencia(ModificarEstadoRequest requestAudiencia);
    Task<bool> InsertarAudienciaAgenda(InsertarAgendaRequest requestAgenda);
    Task<List<ObtenerAgendaFechaDto>> ObtenerAgendaFecha(ObtenerAgendaFechaRequest filtro);
    Task<List<ObtenerDetalleCaracterDto>> ObtenerDetalleCaracter(ObtenerDetalleCaracterRequest request);
    Task<int> ValidarExisteNeun(ValidarNeunRequest request);
    Task<List<ValidarDisponibilidadDto>> ValidarDisponibilidadAudienciaFecha(ValidarDisponibilidadRequest request);
    Task<List<ObtieneResultadoDto>> ObtieneAudienciaResultado(ObtieneResultadoRequest request);
}
