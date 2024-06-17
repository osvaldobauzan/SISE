using Aspose.Pdf;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarCOE;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarSintesisManual;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DetalleNotificacionesFiltros;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.DetalleSintesis;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerAcuerdo;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerSintesisManual;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models.Actuario;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Actuaria.Domain.Entities;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.RepositoryMocks;
public class ActuariaRepositoryMock : IActuariaRepository
{
    public Task<List<ConsultaOficioActuario>> ListaConsultaOficioPorActuario(long asuntoNeunId, int asuntoDocumentoId, long actuarioId, int catOrganismoId)
    {
        var response = new List<ConsultaOficioActuario>();

        return Task.Run(() => { return (response); });
    }

    public Task<bool> AgregarActuario(AgregarActuarioM actuario, long empleadoId)
    {
        return Task.Run(() => { return true; });
    }

    public Task<bool> AgregarActuarioMasivo(AgregarActuarioMasivoM actuario, long empleadoId)
    {
        return Task.Run(() => { return true; });
    }

    public Task<bool> AgregarCOE(AgregarCOEDto coem, long empleadoId)
    {
        return Task.Run(() => { return true; });
    }

    public Task<bool> EditarSintesisAcuerdo(EditarSintesisAcuerdo editarSintesisAcuerdo)
    {
        return Task.Run(() => { return true; });
    }

    public Task<bool> GuardarSintesisAcuerdo(GuardarSintesisAcuerdo guardarSintesisAcuerdo)
    {
        return Task.Run(() => { return true; });
    }

    public Task<bool> InsertarAcuse(SubirAcuseM subirAcuse)
    {
        return Task.Run(() => { return true; });
    }

    public Task<string> InsertarArchivoAcuse(SubirAcuseArchivoM subirAcuse)
    {
        throw new NotImplementedException();
    }

    public Task<(List<FiltroEstado>, List<FiltroContenido>)> ObtenenerFiltrosTablero(int catOrganismoId)
    {
        var listaFiltrosEstado = new List<FiltroEstado>()
        {
            new FiltroEstado()
            {
                Estado = "1"
            },
            new FiltroEstado()
            {
                Estado = "2"
            }
        };

        var listaFiltroContenido = new List<FiltroContenido>()
        {
            new FiltroContenido()
            {
                Id = 1,
                Descripcion = "Test"
            },
            new FiltroContenido()
            {
                Id = 2,
                Descripcion = "Test2"
            }
        };

        return Task.Run(() => { return (listaFiltrosEstado, listaFiltroContenido); });
    }

    public Task<(List<DetalleAcuerdo>, List<Actuaria.Application.Common.Models.Promociones>)> ObtenerDetalleAcuerdo(int CatOrganismoId, long AsuntoNeunId, int SintesisOrden, int AsuntoDocumentoId)
    {
        var listaDetalleAcuerdo = new List<DetalleAcuerdo>()
        {
            new DetalleAcuerdo()
            {
                ContenidoAcuerdo = "1",
                TipoAsuntoDescripcion= "Test"
            },
            new DetalleAcuerdo()
            {
                ContenidoAcuerdo = "2",
                TipoAsuntoDescripcion = "Test2"
            }
        };

        var listaPromociones = new List<Actuaria.Application.Common.Models.Promociones>()
        {
            new Actuaria.Application.Common.Models.Promociones()
            {
                Promocion = "1"
            },
            new Actuaria.Application.Common.Models.Promociones()
            {
                Promocion = "2"
            }
        };

        return Task.Run(() => { return (listaDetalleAcuerdo, listaPromociones); });
    }

    public Task<(List<Notificacion>, MetaDataEstados)> ObtenerNotificacionesConFiltro(ConsultaPaginada consultaPaginada)
    {
        var listaNotificaciones = new List<Notificacion>()
        {
            new Notificacion()
            {
                AsuntoDocumentoId = 1
            },
            new Notificacion()
            {
                AsuntoDocumentoId = 2
            }
        };

        var metadatos = new MetaDataEstados()
        {
            TotalDosDias = 1,
            TotalMasTresDias = 2
        };

        return Task.Run(() => { return (listaNotificaciones, metadatos); });
    }

    public Task<(DatosAsunto datosAsunto, List<NotificacionDetalle> datos, NotificacionDetalleMetaDataEstados metadatos)> ObtenerNotificacionesDetalleConFiltro(ConsultaPaginadaDetalle consultaPaginada)
    {
        var datosAsunto = new DatosAsunto()
        {
            No_Exp = "1",
            NombreArchivo = "NombreTest1"
        };

        var listaDatos = new List<NotificacionDetalle>()
        {
            new NotificacionDetalle()
            {
                NumeroOficio = "20202"
            },
            new NotificacionDetalle()
            {
                NumeroOficio = "20022"
            }
        };

        var metadatos = new NotificacionDetalleMetaDataEstados()
        {
            EnProceso = 1
        };

        return Task.Run(() => { return (datosAsunto, listaDatos, metadatos); });
    }

    public Task<List<RecibirOficiosM>> ObtenerOficiosParaRecibir(int catOrganismoId, string folio, long empleadoId)
    {
        return Task.Run(() =>
        {
            return new List<RecibirOficiosM>()
            {
                new RecibirOficiosM()
                {
                    ConArchivo = 1,
                    AnexoId = 1
                }
            };
        });
    }

    public Task<(List<FiltroTipoParte>, List<FiltroTipoNotificacion>, List<FiltroActuario>)> ObternerFiltroDetalleNotificaciones(DetalleNotificacionesFiltrosConsulta request)
    {
        var listaFiltroTipoParte = new List<FiltroTipoParte>()
        {
            new FiltroTipoParte()
            {
                ID = 1,
                sDescripcion = "Test"
            },
            new FiltroTipoParte()
            {
                ID = 2,
                sDescripcion = "Test2"
            }
        };

        var listaFiltroTipoNotificacion = new List<FiltroTipoNotificacion>()
        {
            new FiltroTipoNotificacion()
            {
                kIdCatNotificaciones = 1,
                sDescripcion = "Test"
            },
            new FiltroTipoNotificacion()
            {
                kIdCatNotificaciones = 2,
                sDescripcion = "Tests2"
            }
        };

        var listaFiltroActuario = new List<FiltroActuario>()
        {
            new FiltroActuario()
            {
                EmpleadoId = 1,
                NombreActuario = "Test",
            },
            new FiltroActuario()
            {
                EmpleadoId = 2,
                NombreActuario = "Tests2"
            }
        };

        return Task.Run(() => { return (listaFiltroTipoParte, listaFiltroTipoNotificacion, listaFiltroActuario); });
    }

    public Task<string> RecibirOficios(int catOrganismoId, long empleadoId, OficiosType oficio)
    {
        return Task.Run(() => { return "Acuse.docx"; });
    }

    public Task<RutasNas> RutaArchivo(string modulo)
    {
        var result = new RutasNas() { Sruta = @"\\10.100.126.204\desa_fs\NotificacionesP4", KId = 192 };
        return Task.FromResult(result);
    }

    Task<(string, long)> IActuariaRepository.InsertarAcuse(SubirAcuseM subirAcuse)
    {
        return Task.FromResult(("archivodeprueba.pdf", 1000l));
    }

    Task<bool> IActuariaRepository.RecibirOficios(int catOrganismoId, long empleadoId, OficiosType oficio)
    {
        return Task.FromResult(true);
    }

    public Task<ObtenerCOE> ConsultaCOE(long NotElectronica)
    {
        var response = new ObtenerCOE();

        return Task.Run(() => { return (response); });
    }

    public Task<List< ObtenerAcuerdoM>> ObtenerAcuerdos(int CatOrganismoId, DateTime fechaInicio, DateTime fechaFin)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AgregarSintesisManual(AgregarSintesisManualDto sintesis)
    {
        throw new NotImplementedException();
    }
    public Task<List<ArchivosAnexos>> ObtenerArchivosyAnexos(long asuntoNeunId, int numeroOrden, int yearPromocion, int catIdOrganismo, int origen, int modulo, int asuntoDocumentoId, int sintesisOden)
    {
        var response = new List<ArchivosAnexos>();

        return Task.Run(() => { return (response); });
    }
    public Task<List<DetalleSintesisDTO>> ObtenerDetalleSintesis(FiltroDetalleSintesis sintesis)
    {
        throw new NotImplementedException();
    }

    public Task<List<ObtenerSintesisManualDTO>> ObtenerSintesisManual(DateTime fechaPublicacion, int CatOrganismoId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> GetStatusUNC(int catIdOrganismo)
    {
        throw new NotImplementedException();
    }
    public Task<PersonasAsunto> ObtenerPersonaAsuntoXidEmpleado(long asuntoNeunId, long parte)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDatosGenerarFolioM> GenerarFoliosPartes(AgregarFoliosPartes info, long empleadoId)
    {
        throw new NotImplementedException();
    }

    public Task<(List<NotificacionesPendientesPorDias>, TotalNotificaciones, List<NotificacionesPorTipo>)> ObtenerNotificacionesPorTipoYPeriodo(int catOrganismoId, long filtroActuarioId, DateTime fechaInicial, DateTime fechaFinal)
    {
        throw new NotImplementedException();
    }

    public Task<List<NotificacionesPorTipoYMes>> ObtenerNotificacionesPorTipoYMes(int catOrganismoId, long filtroActuarioId, DateTime fechaInicial, DateTime fechaFinal)
    {
        throw new NotImplementedException();
    }

    public Task<List<NotificacionesPorTipoYSemana>> ObtenerNotificacionesPorTipoYSemana(int catOrganismoId, long filtroActuarioId, DateTime fechaInicial, DateTime fechaFinal, int mesSeleccionado)
    {
        throw new NotImplementedException();
    }

    public Task<List<Actuario>> ObtenerDetalleIntervalosTiempos(int empleadoId, DateTime fechaInicial, DateTime fechaFinal)
    {
        throw new NotImplementedException();
    }

    public Task<List<ActuarioDetalleLista>> ObtenerDetalleConteos(int catOrganismoId, DateTime fechaInicial, DateTime fechaFinal)
    {
        throw new NotImplementedException();
    }

    public Task<List<DiferenciasTiempos>> ObtenerDetalleIntervalosTiempos(long empleadoId, DateTime fechaInicial, DateTime fechaFinal)
    {
        throw new NotImplementedException();
    }
}
