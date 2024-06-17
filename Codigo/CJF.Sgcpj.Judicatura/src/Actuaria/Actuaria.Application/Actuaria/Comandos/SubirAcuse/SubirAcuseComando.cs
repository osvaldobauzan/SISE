using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.SubirAcuse;

public class SubirAcuseComando : IRequest<bool>
{
    public SubirAcuseDto AcuseDto { get; set; }
}

public interface ISubirAcuseComandoHandler
{
    Task<bool> Handle(SubirAcuseComando request, CancellationToken cancellationToken);
}

public class SubirAcuseComandoHandler : IRequestHandler<SubirAcuseComando, bool>, ISubirAcuseComandoHandler
{
    private readonly IActuariaRepository _actuariaRepository;
    private readonly IMapper _maper;
    private readonly ISesionService _sesionService;
    private readonly INasArchivo _nasArchivo;

    public SubirAcuseComandoHandler(IActuariaRepository actuariaRepository, IMapper maper, ISesionService sesionService, INasArchivo nasArchivo)
    {
        _actuariaRepository = actuariaRepository;
        _maper = maper;
        _sesionService = sesionService;
        _nasArchivo = nasArchivo;
    }
    public async Task<bool> Handle(SubirAcuseComando request, CancellationToken cancellationToken)
    {
        var subirAcuse = _maper.Map<SubirAcuseM>(request.AcuseDto);
        subirAcuse.CatOrganismoId = _sesionService.SesionActual.CatOrganismoId;
        subirAcuse.EmpleadoId = _sesionService.SesionActual.EmpleadoId;

        var (nombreArchivo, notElecId) = await _actuariaRepository.InsertarAcuse(subirAcuse);
        var ruta = await _actuariaRepository.RutaArchivo("Actuaria");
        var archivo = request.AcuseDto.NombreArchivo.Split(".");
        string extension = archivo[1];
        var rutaTemporal = ruta.Sruta + "\\" + subirAcuse.CatOrganismoId + "\\" + nombreArchivo + "." + extension;

        _nasArchivo.AlmacenarArchivo(rutaTemporal, request.AcuseDto.Archivo);

        var subirAcuseArchivo = new SubirAcuseArchivoM()
        {
            ExtensionDocumento = extension,
            IdRuta = ruta.KId,
            NombreArchivo = nombreArchivo,
            NotElecId = notElecId,
            Origen = 1,
            TipoAcuse = subirAcuse.TipoAcuse,
            Usuario = subirAcuse.EmpleadoId,

        };


        await _actuariaRepository.InsertarArchivoAcuse(subirAcuseArchivo);
        return nombreArchivo.Length > 0;
    }
}
