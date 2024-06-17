using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Exceptions;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Common.Enums;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Sentencias.Domain.Entities;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Comandos.SubirSentenciaComando;

public class SubirSentenciaComandoHandler : IRequestHandler<SubirSentenciaComando, SentenciaDto>
{
    private readonly ISentenciasRepository _sentenciasRepository;
    private readonly ISesionService _sessionService;
    private readonly INasArchivo _nasArchivo;
    private readonly IArchivosRepository _archivoRepository;
    private readonly IWordsUtil _wordsUtil;
    private readonly ILogger<SubirSentenciaComandoHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IWordValidator _wordValidator;

    public SubirSentenciaComandoHandler(ISentenciasRepository sentenciasRepository, ISesionService sesionService, INasArchivo nasArchivo, IArchivosRepository archivosRepository, IWordsUtil wordsUtil, ILogger<SubirSentenciaComandoHandler> logger, IMapper mapper, IConfiguration configuration, IWordValidator wordValidator)
    {
        _sentenciasRepository = sentenciasRepository;
        _sessionService = sesionService;
        _nasArchivo = nasArchivo;
        _archivoRepository = archivosRepository;
        _wordsUtil = wordsUtil;
        _logger = logger;
        _mapper = mapper;
        _configuration = configuration;
        _wordValidator = wordValidator;
    }

    public async Task<SentenciaDto> Handle(SubirSentenciaComando request, CancellationToken cancellationToken)
    {
        try
        {
            var errors = new List<ValidationFailure>();
            if (string.IsNullOrEmpty(request.Sentencia) || string.IsNullOrEmpty(request.SentenciaVP))
            {
                errors.Add(new("Sentencia", "Agregue los datos de la sentencia"));
            }

            if (request.ArchivoBytes is null || string.IsNullOrEmpty(request.NomArchivoReal))
            {
                errors.Add(new("Archivo", "Agregue el archivo de la sentencia"));
            }

            if (errors.Any())
            {
                throw new ValidationException(errors);
            }

            var objSentencia = JsonConvert.DeserializeObject<SentenciaDto>(request.Sentencia);
            objSentencia.ArchivoBytes = request.ArchivoBytes;
            objSentencia.NomArchivoReal = request.NomArchivoReal;

            var sentencia = _mapper.Map<Sentencia>(objSentencia);

            var validatorSentencia = new SubirSentenciaComandoValidator(_configuration, _wordValidator);
            var validationSentencia = await validatorSentencia.ValidateAsync(sentencia);

            if (!validationSentencia.IsValid)
            {
                throw new ValidationException(validationSentencia.Errors);
            }

            var objSentenciaVP = JsonConvert.DeserializeObject<SentenciaVersionPublicaDto>(request.SentenciaVP);
            var validatorSVP = new SentenciaVersionPublicaValidator();
            var validationSVP = await validatorSVP.ValidateAsync(objSentenciaVP);
            if (!validationSVP.IsValid)
            {
                throw new ValidationException(validationSVP.Errors);
            }

            sentencia.TipoOrigen = (int)OrigenSentenciaEnum.OrigenSISE2;
            sentencia.IdOrigen = (int)OrigenSentenciaEnum.OrigenSISE2;
            sentencia.UsuarioCaptura = _sessionService.SesionActual.EmpleadoId;
            sentencia.CatOrganismoId = _sessionService.SesionActual.CatOrganismoId;
            sentencia.ExtensionDocumento = Path.GetExtension(sentencia.NomArchivoReal);
            sentencia.TamanioArchivo = sentencia.ArchivoBytes.Length.ToString();
            const char pad = '0';
            sentencia.NombreArchivo = _sessionService.SesionActual.CatOrganismoId.ToString().PadLeft(4, pad) + sentencia.AsuntoNeunId.ToString().PadLeft(12, pad);
            sentencia.IPUsuario = "127.0.0.1";

            //Guardar sentencia
            var registroSentencia = await _sentenciasRepository.GuardarSentencia(sentencia);

            sentencia.AsuntoDocumentoId = registroSentencia.AsuntoDocumentoId;
            sentencia.SintesisOrden = registroSentencia.SintesisOrden;
            sentencia.NombreArchivo = registroSentencia.NombreArchivo;
            sentencia.NumeroOrden = registroSentencia.NumeroOrden;

            if (sentencia.AsuntoDocumentoId == 0)
            {
                return _mapper.Map<SentenciaDto>(sentencia);
            }

            var sentenciaVP = _mapper.Map<SentenciaVP>(objSentenciaVP);
            sentenciaVP.NumeroOrden = sentencia.NumeroOrden;
            sentenciaVP.SintesisOrden = sentencia.SintesisOrden;
            sentenciaVP.TipoOrigen = sentencia.TipoOrigen;
            sentenciaVP.UsuarioCaptura = sentencia.UsuarioCaptura;

            //Guardar sentencia versión pública
            var registroSVP = await _sentenciasRepository.GuardarSentenciaVersionPublica(sentenciaVP);

            if (registroSentencia is not null && !string.IsNullOrEmpty(sentencia.NombreArchivo))
            {

                var rutaSentencias = await _archivoRepository.RutaEscrituraPorModulo("Sentencias");
                var bitacoraSentencia = new RegistroBitacora
                {
                    CatOrganismoId = _sessionService.SesionActual.CatOrganismoId,
                    AsuntoNeunId = sentencia.AsuntoNeunId,
                    TamanioArcivo = sentencia.TamanioArchivo,
                    Carpeta = rutaSentencias,
                    NombreArchvo = sentencia.NombreArchivo,
                    FechaInicia = DateTime.Now,
                    IpCliente = "127.0.0.1",
                    IpHost = "127.0.0.1",
                };

                //Guardar bitácora
                var registroBitacora = await _sentenciasRepository.GuardarBitacora(bitacoraSentencia);

                try
                {
                    var path = rutaSentencias + "\\" + _sessionService.SesionActual.CatOrganismoId.ToString() + "\\" + sentencia.NombreArchivo;
                    //Guardar archivo
                    _nasArchivo.AlmacenarArchivo(path, sentencia.ArchivoBytes);

                    var determinacion = new RegistroDeterminacionJudicial
                    {
                        NombreArchivo = sentencia.NomArchivoReal,
                        IdEstatus = 1,
                        Ip = "127.0.0.1",
                        Observaciones = "Inserción exitosa del Archivo",
                        AsuntoNeunId = sentencia.AsuntoNeunId,
                        NumeroOrden = sentencia.NumeroOrden,
                        SintesisOrden = sentencia.SintesisOrden,
                        ArchivoExtension = sentencia.NombreArchivo,
                        Fojas = 1
                    };

                    //Actualizar sentencia
                    var registrarDeterminacion = await _sentenciasRepository.GuardarDeterminacionJudicial(determinacion);

                    //Relacionar sentencia
                    var relacionarSentencia = new SentenciaSISE3
                    {
                        CatOrganismoId = sentencia.CatOrganismoId,
                        AsuntoNeunId = sentencia.AsuntoNeunId,
                        SintesisOrden = sentencia.SintesisOrden,
                        NumeroOrden = sentencia.NumeroOrden
                    };

                    var relacionar = await _sentenciasRepository.GuardarRelacionSentenciaSISE3(relacionarSentencia);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    //ToDo Rollback
                    throw;
                }
            }

            var result = _mapper.Map<SentenciaDto>(sentencia);
            result.ArchivoBytes = null;
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
