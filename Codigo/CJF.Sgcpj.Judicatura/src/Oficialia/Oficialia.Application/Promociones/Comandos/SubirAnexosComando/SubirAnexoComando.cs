using CJF.Sgcpj.Judicatura.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Exceptions;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;


namespace CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.SubirAnexosComando;
public class SubirAnexosComando : IRequest<List<SubirAnexosDto>>
{
    public long AsuntoNeunId { get; set; }
    public int AsuntoID { get; set; }
    public int YearPromocion { get; set; }
    public int NumeroOrden { get; set; }
    public int CatIdOrganismo { get; set; }
    public int RegistroEmpleadoId { get; set; }
    public int NoRegistro { get; set; }
    public int Origen { get; set; }

    public List<SubirAnexosDto?> Archivos { get; set; }
    public short Fojas { get; set; }
}

public class SubirAnexosComandoHandler : IRequestHandler<SubirAnexosComando, List<SubirAnexosDto>>
{
    private readonly INasArchivo _nas;
    private readonly IPromocionesRepository _promocionesRepository;
    private readonly ISesionService _sesionService;

    public SubirAnexosComandoHandler(INasArchivo nas, IPromocionesRepository promocionesRepository, ISesionService sesionService)
    {
        _nas = nas;
        _promocionesRepository = promocionesRepository;
        _sesionService = sesionService;
    }
    public async Task<List<SubirAnexosDto>> Handle(SubirAnexosComando request, CancellationToken cancellationToken)
    {

        request.CatIdOrganismo = _sesionService.SesionActual.CatOrganismoId;
        request.RegistroEmpleadoId = _sesionService.SesionActual.EmpleadoId;

        List<string> archivosNoGuardados = new List<string>();
        try
        {
                foreach (var archivo in request.Archivos)
                {
              
                    var rutaNas = await _promocionesRepository.RutaArchivo("Oficialia"/*archivo.Descripcion.Id*/);
                    var datosDocumento = new DatosDocumento();
                    var agregarDocumento = new AgregarDocumento()
                    {
                        AsuntoNeunId = request.AsuntoNeunId,
                        AsuntoId = request.AsuntoID,
                        NumeroOrden = request.NumeroOrden,
                        YearPromocion = request.YearPromocion,
                        RegistroEmpleadoId = request.RegistroEmpleadoId,
                        Origen = request.Origen,
                        Caracter = archivo.Caracter.Id,
                        Clase = archivo.TipoAnexo.Id,
                        Descripcion = archivo.Descripcion.Id,
                        CatOrganismoId = request.CatIdOrganismo,
                        NumeroRegistro = request.NoRegistro,
                        Fojas = request.Fojas,
                        NumeroConsecutivo = archivo.Consecutivo
                    };
               
                try
                {
                    datosDocumento = await _promocionesRepository.GuardarDocumento(agregarDocumento);
                    agregarDocumento.NumeroConsecutivo = datosDocumento.NumeroConsecutivo;
                    await _promocionesRepository.ActualizarArchivo(agregarDocumento);
                    if (archivo.Data != null && archivo.Data.Length > 0)
                    {
                        _nas.AlmacenarArchivo(rutaNas + "\\" + request.CatIdOrganismo.ToString() + "\\" +datosDocumento.NombreArchivo, archivo.Data);

                    }
                   
                    archivo.Consecutivo = datosDocumento.NumeroConsecutivo;
                    archivo.guardadoEnBD = true;
                }
                catch (RuleException ex)
                {
                    throw;
                }
                catch (Exception ex)
                {

                    archivosNoGuardados.Add(archivo.NombreArchivo);

                    var rollBackArchivo = new RollBackArchivo()
                    {
                        AsuntoNeunId = request.AsuntoNeunId,
                        AsuntoID = request.AsuntoID,
                        YearPromocion = request.YearPromocion,
                        NumeroOrden = request.NumeroOrden,
                        CatIdOrganismo = request.CatIdOrganismo,
                        RegistroEmpleadoId = request.RegistroEmpleadoId,
                        NumeroRegistro = request.NoRegistro,
                        Consecutivo = datosDocumento.NumeroConsecutivo,
                        Origen = request.Origen
                    };
                    await _promocionesRepository.RollBackArchivo(rollBackArchivo);
                    throw;
                }


            }


        }
        catch (Exception ex)
        {
            throw;
        }




        return await Task.FromResult(request.Archivos);



    }


}
