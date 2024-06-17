using Aspose.Pdf;
using CJF.Sgcpj.Judicatura.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Application.Utils;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Repositories;
using Documentos.Application.Common.Model.GenerarEvidenciaFirma;
using Documentos.Application.FirmadorDocumentos.Comandos.GenerarEvidenciaFirma;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Text.Json;


namespace Documentos.Application.FirmadorDocumentos.Comandos.GuardarArchivoFirmado;
public class GuardarArchivoFirmadoCommand : IRequest<RespuestaGuardadoDto>
{
    public PeticionGuardadoDto PeticionGuardado { get; set; }
}

public class GuardarArchivoFirmadoCommandHandler : IRequestHandler<GuardarArchivoFirmadoCommand, RespuestaGuardadoDto>
{

    private readonly INasArchivo _clienteNas;
    private readonly ITramitesRepository _tramitesRepository;
    private readonly IWordContenido _wordContenido;
    private readonly IPdfService _pdfService;
    private readonly IHojaFirmasService _hojaFirmasService;
    private readonly IPromocionesRepository _promocionesRepository;
    private readonly ILogService _logService;

    public GuardarArchivoFirmadoCommandHandler(INasArchivo clienteNas,
                                               ITramitesRepository tramitesRepository,
                                               IWordContenido wordContenido,
                                               IPdfService pdfService,
                                               IHojaFirmasService hojaFirmasService,
                                               AsposeLicense licence,
                                               IPromocionesRepository promocionesRopository,
                                               ILogService logService)
    {
        _clienteNas = clienteNas;
        _tramitesRepository = tramitesRepository;
        _wordContenido = wordContenido;
        _pdfService = pdfService;
        _hojaFirmasService = hojaFirmasService;
        _promocionesRepository = promocionesRopository;
        _logService = logService;
        Aspose.Words.License license = new Aspose.Words.License();
        license.SetLicense(licence.GetLicense());

        Aspose.Pdf.License licensePdf = new Aspose.Pdf.License();
        licensePdf.SetLicense(licence.GetLicense());
    }
    public async Task<RespuestaGuardadoDto> Handle(GuardarArchivoFirmadoCommand request, CancellationToken cancellationToken)
    {

        RespuestaGuardadoDto respuestaGuardado = new RespuestaGuardadoDto();
        var modulo = Int32.Parse(request.PeticionGuardado.id.Substring(0, 1));
        request.PeticionGuardado.id = request.PeticionGuardado.id.Substring(2);
        var guidDoc = new Guid(request.PeticionGuardado.id);

        if (modulo == 1)
        {
            var tipoDocumento = "promocion";
            string contenidoBase64 = request.PeticionGuardado.Contenido;
            var archivo = await _tramitesRepository.ObtenerTramitePorIdModuloTipoAsync(guidDoc, 1, tipoDocumento);
            if (archivo != null && archivo.Any())
            {
                var archivoEncontrado = archivo.First();
                string uncPath = archivoEncontrado.SRuta + "\\" + archivoEncontrado.CatOrganismoId + "\\" + request.PeticionGuardado.Nombre;
                _clienteNas.AlmacenarArchivo(uncPath, Convert.FromBase64String(request.PeticionGuardado.Contenido));
                //var promocion = new EstadoOficioRoot() { EstadoOficio = new EstadoOficio { Firmado = true, GuidDocumento = request.PeticionGuardado.id, Extension = ".pdf" } };
                GenerarPdfHojaFirmas(archivoEncontrado, contenidoBase64, request.PeticionGuardado.id, modulo);
                await _promocionesRepository.ActualizaEstadoFirmaPromocion(request.PeticionGuardado.id, archivoEncontrado.CatOrganismoId, archivoEncontrado.AsuntoNeunId, archivoEncontrado.NombreArchivo);
                respuestaGuardado.Status = true;
                respuestaGuardado.Data = true;
                respuestaGuardado.Mensaje = "Archivo guardado con éxtio";
            }
            else
            {
                respuestaGuardado.Status = false;
                respuestaGuardado.Data = false;
                respuestaGuardado.Mensaje = "Archivo guardado no encontrado";
            }
        }
        else if (modulo == 2)
        {
            var tipoDocumento = "acuerdo";
            if (request.PeticionGuardado.Nombre.Contains("oficio"))
            {
                tipoDocumento = "oficio";
            }
            string contenidoBase64p7m = request.PeticionGuardado.Contenido;
            var archivo = await _tramitesRepository.ObtenerTramitePorIdModuloTipoAsync(guidDoc, 2, tipoDocumento);
            if (archivo != null && archivo.Any())
            {
                var archivoEncontrado = archivo.First();
                //Guardado de P7M 
                string uncPath = archivoEncontrado.SRuta + "\\" + archivoEncontrado.CatOrganismoId + "\\" + request.PeticionGuardado.Nombre;
                _clienteNas.AlmacenarArchivo(uncPath, Convert.FromBase64String(request.PeticionGuardado.Contenido));
                if (tipoDocumento == "acuerdo")
                {
                    var acuerdo = new DeterminacionAcuerdoRoot() { DeterminacionAcuerdo = new DeterminacionAcuerdo() { Firmado = true, GUID = guidDoc } };

                    if (archivoEncontrado.Firmado)
                    {
                        acuerdo.DeterminacionAcuerdo.Extension = ".pdf"; //se prepara la bd para servir el acuerdo firmado
                        acuerdo.DeterminacionAcuerdo.Nombre = archivoEncontrado.NombreArchivo.Replace(".docx", "").Replace(".doc", "").Replace(".pdf", "");
                        GenerarPdfHojaFirmas(archivoEncontrado, contenidoBase64p7m, request.PeticionGuardado.id, modulo);
                    }

                    await _tramitesRepository.ActualizaDeterminacion(acuerdo);
                }
                else
                {
                    var oficio = new EstadoOficioRoot() { EstadoOficio = new EstadoOficio { Firmado = true, GuidDocumento = guidDoc, Extension = ".pdf" } };
                    GenerarPdfHojaFirmas(archivoEncontrado, contenidoBase64p7m, request.PeticionGuardado.id.ToString(), modulo);
                    await _tramitesRepository.ActualizaEstadoOficio(oficio);
                }

                respuestaGuardado.Status = true;
                respuestaGuardado.Data = true;
                respuestaGuardado.Mensaje = "Archivo guardado con éxito";
            }
            else
            {
                respuestaGuardado.Status = false;
                respuestaGuardado.Data = false;
                respuestaGuardado.Mensaje = "Archivo guardado no encontrado";
            }
        }
        else
        {
            respuestaGuardado.Status = false;
            respuestaGuardado.Data = false;
            respuestaGuardado.Mensaje = "Archivo guardado no encontrado, la variable modulo no seencontró";
        }
        return respuestaGuardado;
    }

    private void GenerarPdfHojaFirmas(Tramite archivoEncontrado, string contenidoBase64p7m, string guid, int? modulo)
    {
        MemoryStream acuerdoPdfMs, hojaFirmasStream;
        var generarEvidienciaFirmaDTO = _hojaFirmasService.GenerarHojaFirmas(
            new GenerarEvidienciaFirmaFiltro()
            {
                p7m = contenidoBase64p7m,
                ArchivoFirmado = archivoEncontrado.NombreArchivo,
                GuidDocumento = guid
            });
        string[] firmas = new string[] { generarEvidienciaFirmaDTO.PrimerFirmante, generarEvidienciaFirmaDTO.Hash, generarEvidienciaFirmaDTO.Fecha ?? CJF.Sgcpj.Judicatura.Common.Application.Utils.DateTimeUtil.ObtenerHoraLocal().ToString("dd/MM/yyyy") };
        var wordAcuerdo = _clienteNas.ObtenerArchivoComoBase64String(archivoEncontrado.RutaCompleta);//se obtiene el doc original
        byte[] acuerdoPdf = _wordContenido.SaveWordtoPdf(Convert.FromBase64String(wordAcuerdo.Base64));// se onvierte a base 64
        if (modulo == 1)
        {
            var rutaN = archivoEncontrado.SRuta + "\\" + archivoEncontrado.CatOrganismoId + "\\" + "(original)" + archivoEncontrado.NombreArchivo;
            _clienteNas.AlmacenarArchivo(rutaN, acuerdoPdf);
        }
        acuerdoPdf = _pdfService.AddSings(acuerdoPdf, firmas);

        var hojaFirmasEnBase64 = Convert.FromBase64String(generarEvidienciaFirmaDTO.Contenido);
        acuerdoPdfMs = new MemoryStream(acuerdoPdf);
        hojaFirmasStream = new MemoryStream(hojaFirmasEnBase64);

        var pdfHojaFirmas = new Document(hojaFirmasStream);
        var acuerdoPdfCompleto = new Document(acuerdoPdfMs);

        acuerdoPdfCompleto.Pages.Add(pdfHojaFirmas.Pages);//se le agregan las firmas

        MemoryStream stream = new MemoryStream();
        acuerdoPdfCompleto.Save(stream);
        stream.Seek(0, SeekOrigin.Begin);
        var bytesAcuerdo = Functions.ConvertStreamToByteArray(stream);
        string uncPath = archivoEncontrado.SRuta + "\\" + archivoEncontrado.CatOrganismoId + "\\" + archivoEncontrado.NombreArchivo.Replace(".docx", "").Replace(".doc", "").Replace(".pdf", "");
        uncPath = uncPath + ".pdf";
        _clienteNas.AlmacenarArchivo(uncPath, bytesAcuerdo);
    }


    void RegistrarLog(TipoMovimiento tipoMovimiento, string request, string accion, string? response = null)
    {
        _logService.RegistrarEvento(
        new DatosLog
        {
            TipoMovimiento = tipoMovimiento,
            IdUsuario = 0,//_sessionService.SesionActual.EmpleadoId,
            DatosEntrada = request,
            DatosSalida = response,
            ModuloOrigen = $"{GetType().Name} - {accion}"
        }).ConfigureAwait(false).GetAwaiter();
    }
}