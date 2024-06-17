using System.Reflection;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.AgregarCOE;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.EditarSintesis;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.GuardarSintesis;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Comandos.SubirAcuse;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.Actuario;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.GenerarOficio;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.NotificacionDetalle;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerCOE;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerNotificacionesConFiltro;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.ObtenerPersonaAsunto;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Actuaria.Consulta.RecibirOficios;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models.Actuario;
using CJF.Sgcpj.Judicatura.Actuaria.Domain.Entities;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Common.Application.Utils;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        MapTableroActuaria();
        MapGuardarSintesis();
        MapEditarSintesis();
        MapAgregarCOE();
        MapConsultaCOE();
        MapRecibirOficios();
        MapObtenerPersonaAsunto();
    }

    private void MapTableroActuaria()
    {
        CreateMap<ObtieneNotificacionesConFiltroConsulta, ConsultaPaginada>()
            .ForMember(d => d.FechaInicial, o => o.MapFrom(s => MappingUtils.ObtenerFechaDeCadena(s.FechaInicial)))
            .ForMember(d => d.FechaFinal, o => o.MapFrom(s => MappingUtils.ObtenerFechaDeCadena(s.FechaFinal)));
        CreateMap<NotificacionDetalleConsulta, ConsultaPaginadaDetalle>();

        CreateMap<ActuariaItemTablero, Expediente>()
            .ForMember(d => d.AsuntoNeunId, o => o.MapFrom(s => s.AsuntoNeunId))
            .ForMember(d => d.AsuntoAlias, o => o.MapFrom(s => s.No_Exp))
            .ForMember(d => d.TipoProcedimiento, o => o.MapFrom(s => s.TipoProcedimiento))
            .ForMember(d => d.CatTipoAsunto, o => o.MapFrom(s => s.TipoAsuntoDescripcion))
            .ForMember(d => d.CatTipoAsuntoId, o => o.MapFrom(s => s.CatTipoAsuntoId));
        CreateMap<ActuariaItemTablero, Notificacion>()
            .ForPath(d => d.Expediente.AsuntoNeunId, o => o.MapFrom(s => s.AsuntoNeunId))
            .ForPath(d => d.Expediente.AsuntoAlias, o => o.MapFrom(s => s.No_Exp))
            .ForPath(d => d.Expediente.TipoProcedimiento, o => o.MapFrom(s => s.TipoProcedimiento))
            .ForPath(d => d.Expediente.CatTipoAsunto, o => o.MapFrom(s => s.TipoAsuntoDescripcion))
            .ForPath(d => d.Expediente.CatTipoAsuntoId, o => o.MapFrom(s => s.CatTipoAsuntoId))
            .ForMember(d => d.NumeroRegistro, o => o.MapFrom(s => s.NumeroRegistro))
            .ForMember(d => d.NombreArchivo, o => o.MapFrom(s => s.NombreArchivo))
            .ForMember(d => d.TipoContenidoDescripcion, o => o.MapFrom(s => s.TipoContenidoDescripcion))
            .ForMember(d => d.FechaAuto_F, o => o.MapFrom(s => s.FechaAuto_F))
            .ForMember(d => d.FechaAutoriza, o => o.MapFrom(s => s.FechaAutoriza))
            .ForMember(d => d.Transcurrido, o => o.MapFrom(s => s.Transcurrido))
            .ForMember(d => d.Notificados, o => o.MapFrom(s => s.Notificados))
            .ForMember(d => d.Estado, o => o.MapFrom(s => s.Estado))
            .ForMember(d => d.Sintesis, o => o.MapFrom(s => s.Sintesis))
            .ForMember(d => d.TipoCuaderno, o => o.MapFrom(s => s.TipoCuaderno))
            .ForMember(d => d.TipoCuadernoDesc, o => o.MapFrom(s => s.TipoCuadernoDesc))
            .ForMember(d => d.SecretarioPId, o => o.MapFrom(s => s.SecretarioPId))
            .ForMember(d => d.ContenidoId, o => o.MapFrom(s => s.ContenidoId))
            .ForMember(d => d.Contenido, o => o.MapFrom(s => s.Contenido))
            .ForMember(d => d.UsuarioCaptura, o => o.MapFrom(s => s.UsuarioCaptura))
            .ForMember(d => d.AsuntoDocumentoId, o => o.MapFrom(s => s.AsuntoDocumentoId))
            .ForMember(d => d.SintesisOrden, o => o.MapFrom(s => s.SintesisOrden))
            .ForMember(d => d.ConAcuse, o => o.MapFrom(s => s.ConAcuse))
            .ForMember(d => d.FechaAlta, o => o.MapFrom(s => s.FechaAlta))
            .ForMember(d => d.uGuidDocumento, o => o.MapFrom(s => s.uGuidDocumento));

        CreateMap<ContadoresEstadosTablero, MetaDataEstados>()
            .ForMember(d => d.TotalNotificaciones, o => o.MapFrom(s => s.Todos))
            .ForMember(d => d.TotalMasTresDias, o => o.MapFrom(s => s.TresDias))
            .ForMember(d => d.TotalDosDias, o => o.MapFrom(s => s.DosDias))
            .ForMember(d => d.TotalUnDia, o => o.MapFrom(s => s.UnDia))
            .ForMember(d => d.TotalNotificados, o => o.MapFrom(s => s.Notificados));

        CreateMap<SubirAcuseDto, SubirAcuseM>()
            .ForMember(d => d.FechaNotificacion, o => o.MapFrom(s => MappingUtils.ObtenerFechaDeCadena(s.FechaNotificacion)))
            .ForMember(d => d.FechaNotificacionCitatorio, o => o.MapFrom(s => MapFechaNull(s)));

        CreateMap<ActuarioNotificacionesConsulta, ConsultaPaginadaNotificaciones>()
            .ForMember(d => d.FechaInicial, o => o.MapFrom(s => MappingUtils.ObtenerCadenaDeFecha(s.FechaInicial, "yyyy-MM-dd") ))
            .ForMember(d => d.FechaFinal, o => o.MapFrom(s => MappingUtils.ObtenerCadenaDeFecha(s.FechaFinal, "yyyy-MM-dd")));

        CreateMap<PersonasDto, ParteNotificacionFolios>()
            .ForMember(d => d.ParteId, o => o.MapFrom(s => s.PersonaId))
            .ForMember(d => d.PromoventeId, o => o.MapFrom(s => s.PromoventeId))
            .ForMember(d => d.TipoNotificacionId, o => o.MapFrom(s => s.TipoNotificacionId))
            .ForMember(d => d.TipoAnexoId, o => o.MapFrom(s => s.TipoAnexoId))
            .ForMember(d => d.TextoOficioLibre, o => o.MapFrom(s => s.TextoOficioLibre))
            .ForMember(d => d.NombreParte, o => o.MapFrom(s => s.NombreParte));
    }

    private static DateTime? MapFechaNull(SubirAcuseDto s)
    {
        if (string.IsNullOrEmpty(s.FechaNotificacionCitatorio))
        {
            return null;
        }
        return MappingUtils.ObtenerFechaDeCadena(s.FechaNotificacionCitatorio);
    }

    private void MapGuardarSintesis()
    {
        CreateMap<GuardarSintesisDto, GuardarSintesisAcuerdo>()
            .ForMember(d => d.AsuntoNeunId, o => o.MapFrom(s => s.AsuntoNeunId))
            .ForMember(d => d.TipoCuaderno, o => o.MapFrom(s => s.TipoCuaderno))
            .ForMember(d => d.NombreDocumento, o => o.MapFrom(s => s.NombreDocumento))
            .ForMember(d => d.ExtensionDocumento, o => o.MapFrom(s => s.ExtensionDocumento))
            .ForMember(d => d.Contenido, o => o.MapFrom(s => s.Contenido))
            .ForMember(d => d.FechaAcuerdo, o => o.MapFrom(s => s.FechaAcuerdo))
            .ForMember(d => d.UsuarioCaptura, o => o.MapFrom(s => s.UsuarioCaptura))
            .ForMember(d => d.AsuntoDocumentoId, o => o.MapFrom(s => s.AsuntoDocumentoId))
            .ForMember(d => d.Sintesis, o => o.MapFrom(s => s.Sintesis))
            .ForMember(d => d.SintesisOrden, o => o.MapFrom(s => s.SintesisOrden))
            .ForMember(d => d.NombreArchivo, o => o.MapFrom(s => s.NombreArchivo));
    }
    private void MapEditarSintesis()
    {
        CreateMap<EditarSintesisDto, EditarSintesisAcuerdo>()
            .ForMember(d => d.AsuntoNeunId, o => o.MapFrom(s => s.AsuntoNeunId))
            .ForMember(d => d.TipoCuaderno, o => o.MapFrom(s => s.TipoCuaderno))
            .ForMember(d => d.NombreDocumento, o => o.MapFrom(s => s.NombreDocumento))
            .ForMember(d => d.ExtensionDocumento, o => o.MapFrom(s => s.ExtensionDocumento))
            .ForMember(d => d.Contenido, o => o.MapFrom(s => s.Contenido))
            .ForMember(d => d.FechaAcuerdo, o => o.MapFrom(s => s.FechaAcuerdo))
            .ForMember(d => d.UsuarioCaptura, o => o.MapFrom(s => s.UsuarioCaptura))
            .ForMember(d => d.AsuntoDocumentoId, o => o.MapFrom(s => s.AsuntoDocumentoId))
            .ForMember(d => d.Sintesis, o => o.MapFrom(s => s.Sintesis))
            .ForMember(d => d.SintesisOrden, o => o.MapFrom(s => s.SintesisOrden))
            .ForMember(d => d.NombreArchivo, o => o.MapFrom(s => s.NombreArchivo));
    }

    private void MapAgregarCOE()
    {
        CreateMap<AgregarCOEDto, AgregarCOEM>()
         .ForMember(d => d.NotElecId, o => o.MapFrom(s => s.NotElecId))
         .ForMember(d => d.Expediente, o => o.MapFrom(s => s.Expediente))
         .ForMember(d => d.TipoComunicacion, o => o.MapFrom(s => s.TipoComunicacion))
         .ForMember(d => d.NumeroOrigen, o => o.MapFrom(s => s.NumeroOrigen))
         .ForMember(d => d.FechaEnvio, o => o.MapFrom(s => s.FechaEnvio))
         .ForMember(d => d.Secretario, o => o.MapFrom(s => s.Secretario))
         .ForMember(d => d.Mesa, o => o.MapFrom(s => s.Mesa))
         .ForMember(d => d.TipoAsunto, o => o.MapFrom(s => s.TipoAsunto))
         .ForMember(d => d.NumeroExpedienteOrigen, o => o.MapFrom(s => s.NumeroExpedienteOrigen))
         .ForMember(d => d.Destinatario, o => o.MapFrom(s => s.Destinatario))
         .ForMember(d => d.Objetivo, o => o.MapFrom(s => s.Objetivo))
         .ForMember(d => d.OficinaCorrespondenciaComun, o => o.MapFrom(s => s.OficinaCorrespondenciaComun));
    }

    private void MapConsultaCOE()
    {
        CreateMap<ObtenerCOE, ObtenerCOEDto>().ReverseMap();
    }

    private void MapRecibirOficios()
    {
        CreateMap<RecibirOficiosM, RecibirOficiosDto>()
            .ForMember(dest => dest.IdEmpleadoRecepcion, options => options.MapFrom(orderItem => orderItem.IdEmpleadoRecepcion == null ? 0 : orderItem.IdEmpleadoRecepcion))
            .ForMember(dest => dest.Nombre, options => options.MapFrom(orderItem => orderItem.Nombre ?? string.Empty))
            .ForMember(dest => dest.ApellidoPaterno, options => options.MapFrom(orderItem => orderItem.ApellidoPaterno ?? string.Empty))
            .ForMember(dest => dest.ApellidoMaterno, options => options.MapFrom(orderItem => orderItem.ApellidoMaterno ?? string.Empty));
    }

    private void MapObtenerPersonaAsunto()
    {

        CreateMap<ListarPersonaAsuntoDto, PersonasAsunto>()
           .ForMember(d => d.AMaterno, o => o.MapFrom(s => s.AMaterno))
           .ForMember(d => d.APaterno, o => o.MapFrom(s => s.APaterno));

    }
    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var mapFromType = typeof(IMapFrom<>);

        var mappingMethodName = nameof(IMapFrom<object>.Mapping);

        bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;

        var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

        var argumentTypes = new Type[] { typeof(Profile) };

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod(mappingMethodName);

            if (methodInfo != null)
            {
                methodInfo.Invoke(instance, new object[] { this });
            }
            else
            {
                var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                if (interfaces.Count > 0)
                {
                    foreach (var @interface in interfaces)
                    {
                        var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

                        interfaceMethodInfo?.Invoke(instance, new object[] { this });
                    }
                }
            }
        }
    }
}
