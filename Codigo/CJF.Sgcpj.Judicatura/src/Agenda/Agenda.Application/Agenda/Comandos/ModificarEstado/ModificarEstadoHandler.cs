using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Comandos.InsertarAgenda;
using CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Consulta.ValidarAudiencia;
using CJF.Sgcpj.Judicatura.Agenda.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Agenda.Application.Agenda.Comandos.ModificarEstado;
public class ModificarEstadoHandler : IRequestHandler<ModificarEstadoRequest, ResultadoModificarEstadoDto>
{
    private readonly IAgendaRepository _repository;
    private readonly IMapper _mapper;
    private readonly ISesionService _sesionService;
    private readonly ILogger _logger;

    public ModificarEstadoHandler(IAgendaRepository reposiroty, IMapper mapper, ISesionService sesionService, ILogger logger)
    {
        _repository = reposiroty;
        _mapper = mapper;
        _sesionService = sesionService;
        _logger = logger;
    }

    public async Task<ResultadoModificarEstadoDto> Handle(ModificarEstadoRequest requestAudiencia, CancellationToken cancellationToken)
    {
        ResultadoModificarEstadoDto resultEstadoAudiencia = new ResultadoModificarEstadoDto();

        try
        {
            var resultEstado = await _repository.ModificarEstadoAudiencia(requestAudiencia);

            if (resultEstado != null)
            {
                if (resultEstado.IdResultado == requestAudiencia.IdResultado)
                {
                    resultEstadoAudiencia.EsExito = true;
                    resultEstadoAudiencia.ResultadoAudiencia = resultEstado;
                    resultEstadoAudiencia.MessageResultado = "El cambio de estado se hizo correctamente";
                }
                else
                {
                    resultEstadoAudiencia.EsExito = false;
                    resultEstadoAudiencia.ResultadoAudiencia = null;
                    resultEstadoAudiencia.MessageResultado = "No se realizo el cambio de estado se hizo correctamente";
                }
            }
            else
            {
                resultEstadoAudiencia.EsExito = false;
                resultEstadoAudiencia.ResultadoAudiencia = null;
                resultEstadoAudiencia.MessageResultado = "No se realizo el cambio de estado se hizo correctamente";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message.ToString(), requestAudiencia);
        }
        return resultEstadoAudiencia;
    }



}
