using AutoMapper;
using CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ValidarAudiencia;
using CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ValidarNeun;
using CJF.Sgcpj.Judicatura.Agenda.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Comandos.InsertarAgenda;
public class InsertarAgendaHandler : IRequestHandler<InsertarAgendaRequest, ResultadoInsertarAgendaDto>
{
    private readonly IAgendaRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    private readonly ILogger _logger;

    public InsertarAgendaHandler(IAgendaRepository reposiroty, IMapper mapper, ISesionService sesionService, ILogger logger)
    {
        _repository = reposiroty;
        _mapper = mapper;
        _sesionService = sesionService;
        _logger = logger;
    }

    public async Task<ResultadoInsertarAgendaDto> Handle(InsertarAgendaRequest request, CancellationToken cancellationToken)
    {
        ResultadoInsertarAgendaDto resultAgenda = new ResultadoInsertarAgendaDto();
        ObtieneResultadoDto agendaResultado = new ObtieneResultadoDto();
        ValidarNeunRequest neunRequest = new ValidarNeunRequest();
        bool resultInsert = false;

        try
        {
            neunRequest.IdNeun = request.NumeroNeun;
            var EsValidoNeun = ValidarExisteNeun(neunRequest);

            if (EsValidoNeun.Result == 0)
            {
                resultAgenda.MessageResultado = "El IdNeun " + request.NumeroNeun + " no es valido.";
            }
            else
            {
                var resultDisponibilidad = ValidarDisponibilidadAgenda(request);

                if (resultDisponibilidad.Result.Count == 0)
                {
                    agendaResultado = ObtieneAudienciaPorResultado(request).Result.FirstOrDefault();

                    if (agendaResultado != null && agendaResultado.Resultado == 0)
                    {
                        ResultadoDto itemsResultado = new ResultadoDto
                        {
                            IdAgenda = agendaResultado.IdAgenda,
                            IdAudiencia = agendaResultado.IdAudiencia,
                            IdResultado = agendaResultado.Resultado,
                            Expedinte = agendaResultado.Expediente,
                            FechaAudiencia = agendaResultado.FechaAudiencia
                        };
                        resultAgenda.ResultadoAudiencia = itemsResultado;
                        resultAgenda.MessageResultado = "Se ha detectado una audiencia sin resultado para el expediente: " + agendaResultado.Expediente + " del día " + agendaResultado.FechaAudiencia + ". Te recordamos que para agendar una audiencia debe estar marcado el estado del anterior ¿Deseas marcar un estado ahora?";
                    }
                    else
                    {
                        DateTime fechaNueva = agendaResultado != null ? Convert.ToDateTime(agendaResultado.FechaAudiencia) : Convert.ToDateTime(DateTime.Now);

                        if (request.FechaAudiencia.CompareTo(fechaNueva) >= 0)
                        {
                            resultInsert = await _repository.InsertarAudienciaAgenda(request);
                            if (resultInsert)
                                resultAgenda.MessageResultado = "La audiencia del expediente:  de Tipo asunto:   y Tipo procedimiento se agendó para el día dd/mm/aaaa a las HH:MM hrs.";
                            else
                                resultAgenda.MessageResultado = "No se logro realizar el registro de la audiencia exitosamente";
                        }
                        else
                        {
                            resultAgenda.MessageResultado = "Ya existe una audiencia para el expediente: " + request.Expendiente + " del día " + request.FechaAudiencia.ToString() + " Recuerda que no se puede agendar fechas previas a la última audiencia disponible";
                        }
                    }
                }
                else
                {
                    var msgDisponibilidad = resultDisponibilidad.Result.FirstOrDefault();
                    resultAgenda.MessageResultado = "La audiencia constitucional del expediente: " + request.Expendiente + "ha sido asiganado para el día: " + msgDisponibilidad.FechaAudiencia + " " + msgDisponibilidad.HoraAudiencia + " por " + msgDisponibilidad.Empleado;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,ex.Message.ToString(),request);
        }
        return resultAgenda;
    }

    public async Task<int> ValidarExisteNeun(ValidarNeunRequest requestNeun)
    {
        int resultNeun = 0;
        try
        {
            resultNeun = await _repository.ValidarExisteNeun(requestNeun);
           
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message.ToString(), requestNeun);
        }
        return resultNeun;
    }

    public async Task<List<ValidarDisponibilidadDto>> ValidarDisponibilidadAgenda(InsertarAgendaRequest request)
    {
        List<ValidarDisponibilidadDto> lstResultFecha = new List<ValidarDisponibilidadDto>();

        try
        {
            ValidarDisponibilidadRequest requestDisponibilidad = new ValidarDisponibilidadRequest
            {
                FechaAudiencia = request.FechaAudiencia,
                HoraAudiencia = request.HoraAudiencia,
            };

            lstResultFecha = await _repository.ValidarDisponibilidadAudienciaFecha(requestDisponibilidad);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message.ToString(), request);
        }
        return lstResultFecha;
    }

    public async Task<List<ObtieneResultadoDto>> ObtieneAudienciaPorResultado(InsertarAgendaRequest request)
    {
        List<ObtieneResultadoDto> lstResultValidacion = new List<ObtieneResultadoDto>();
        try
        {
            ObtieneResultadoRequest requestResultado = new ObtieneResultadoRequest
            {
                Neun = request.NumeroNeun,
                IdTipoAudiencia = request.IdTipoAudiencia,
                UsaPartes = request.UsaPartes
            };
            lstResultValidacion = await _repository.ObtieneAudienciaResultado(requestResultado);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message.ToString(), request);
        }

        return lstResultValidacion;
    }
}
