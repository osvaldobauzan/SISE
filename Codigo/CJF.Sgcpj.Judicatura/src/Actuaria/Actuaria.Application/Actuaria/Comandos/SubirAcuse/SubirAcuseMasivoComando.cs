using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.SubirAcuse;


public class SubirAcuseMasivoComando : IRequest<List<bool>>
{
    public SubirAcuseMasivoDto AcusePartesDto { get; set; }
}

public class SubirAcuseMasivoComandoHandler : IRequestHandler<SubirAcuseMasivoComando, List<bool>>
{
    private readonly IActuariaRepository _actuariaRepository;
    private readonly IMapper _maper;
    private readonly ISesionService _sesionService;
    private readonly INasArchivo _nasArchivo;
    private readonly ISubirAcuseComandoHandler _subirAcuseComandoHandler;

    public SubirAcuseMasivoComandoHandler(IActuariaRepository actuariaRepository, IMapper maper, 
        ISesionService sesionService, INasArchivo nasArchivo, ISubirAcuseComandoHandler subirAcuseComandoHandler)
    {
        _actuariaRepository = actuariaRepository;
        _maper = maper;
        _sesionService = sesionService;
        _nasArchivo = nasArchivo;
        _subirAcuseComandoHandler = subirAcuseComandoHandler;
    }

    public async Task<List<bool>> Handle(SubirAcuseMasivoComando request, CancellationToken cancellationToken)
    {
        var notificaciones = request.AcusePartesDto.PartesNotificacionesAcuse;
        var archivosAcuse = request.AcusePartesDto.ArchivosAcuse;

        var resultados = new List<bool>();

        foreach (var noti in notificaciones)
        {
            try
            {
                SubirAcuseComando subirAcuseComando = new SubirAcuseComando();

                SubirAcuseDto subirAcuerdoDto = new SubirAcuseDto()
                {
                    AsuntoNeunId = request.AcusePartesDto.AsuntoNeunId,
                    SintesisCitatorio = request.AcusePartesDto.SintesisCitatorio,
                    TipoAcuse = request.AcusePartesDto.TipoAcuse,
                    SintesisOrden = request.AcusePartesDto.SintesisOrden,
                    FechaNotificacion = noti.FechaNotificacion,
                    FechaNotificacionCitatorio = request.AcusePartesDto.FechaNotificacionCitatorio,
                    PersonaId = noti.ParteId,
                    TipoNotificacion = request.AcusePartesDto.TipoNotificacion,
                    Archivo = archivosAcuse.First().Archivo,
                    NombreArchivo = archivosAcuse.First().NombreArchivo,
                };

                SubirAcuseComando requestSubirAcuse = new SubirAcuseComando()
                {
                    AcuseDto = subirAcuerdoDto
                };

                resultados.Add(await _subirAcuseComandoHandler.Handle(requestSubirAcuse, new CancellationToken()));

            }
            catch (Exception)
            {
                resultados.Add(false);
            }
        }
        
        return resultados;
    }
}
