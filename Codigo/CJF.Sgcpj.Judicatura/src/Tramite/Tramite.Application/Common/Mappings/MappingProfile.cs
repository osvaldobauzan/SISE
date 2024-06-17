using System.Reflection;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Common.Application.Utils;
using CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Comandos.SubirAcuerdoComando;
using CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Consulta.ObtenerTramitesConFiltro;
using CJF.Sgcpj.Judicatura.Tramite.Domain.Entities;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        MapTableroTramite();
        MapSubirAcuerdo();

    }


    private void MapTableroTramite()
    {
        CreateMap<ObtieneTramitesConFiltroConsulta, ConsultaPaginadaTramite>()
           .ForMember(d => d.FechaInicial, o => o.MapFrom(s => MappingUtils.ObtenerFechaDeCadena(s.FechaInicial)))
          .ForMember(d => d.FechaFinal, o => o.MapFrom(s => MappingUtils.ObtenerFechaDeCadena(s.FechaFinal)));

        

        CreateMap<TramiteItemTablero, Expediente>()
            .ForMember(d => d.AsuntoNeunId, o => o.MapFrom(s => s.AsuntoNeunId))
            .ForMember(d => d.AsuntoAlias, o => o.MapFrom(s => s.No_Exp))
            .ForMember(d => d.CatTipoAsunto, o => o.MapFrom(s => s.TipoAsuntoDescripcion))
            .ForMember(d => d.CatTipoAsuntoId, o => o.MapFrom(s => s.TipoAsuntoId))
            .ForMember(d => d.NombreCorto, o => o.MapFrom(s => s.NombreTipoCuaderno))
            .ForMember(d => d.AsuntoNeunId, o => o.MapFrom(s => s.AsuntoNeunId));
        CreateMap<TramiteItemTablero, CJF.Sgcpj.Judicatura.Tramite.Domain.Entities.Tramite>()
            .ForPath(d => d.Expediente.AsuntoNeunId, o => o.MapFrom(s => s.AsuntoNeunId))
            .ForPath(d => d.Expediente.AsuntoAlias, o => o.MapFrom(s => s.No_Exp))
            .ForPath(d => d.Expediente.CatTipoAsunto, o => o.MapFrom(s => s.TipoAsuntoDescripcion))
            .ForPath(d => d.Expediente.CatTipoAsuntoId, o => o.MapFrom(s => s.TipoAsuntoId))
            .ForPath(d => d.Expediente.NombreCorto, o => o.MapFrom(s => s.NombreTipoCuaderno))
            .ForPath(d => d.Expediente.TipoProcedimiento, o => o.MapFrom(s => s.TipoProcedimiento))
            .ForMember(d => d.NumeroRegistro, o => o.MapFrom(s => s.NumeroRegistro))
            .ForMember(d => d.TipoPromocionDescripcion, o => o.MapFrom(s => s.TipoPromocionDescripcion))
            .ForMember(d => d.FechaRecibido, o => o.MapFrom(s => s.FechaRecibido))
            .ForMember(d => d.NumeroOrden, o => o.MapFrom(s => s.NumeroOrden))
            .ForMember(d => d.NombreTipoCuaderno, o => o.MapFrom(s => s.NombreTipoCuaderno))
            .ForMember(d => d.NombreParte, o => o.MapFrom(s => s.NombreParte))
            .ForMember(d => d.TipoContenidoDescripcion, o => o.MapFrom(s => s.TipoContenidoDescripcion))
            .ForMember(d => d.Contenido, o => o.MapFrom(s => s.Contenido))
            .ForMember(d => d.Copias, o => o.MapFrom(s => s.Copias))
            .ForMember(d => d.Anexos, o => o.MapFrom(s => s.Anexos))
            .ForMember(d => d.Estado, o => o.MapFrom(s => s.Estado))
            .ForMember(d => d.Mesa, o => o.MapFrom(s => s.Mesa))
            .ForMember(d => d.SecretarioId, o => o.MapFrom(s => s.SecretarioId))
            .ForMember(d => d.SecretarioDescripcion, o => o.MapFrom(s => s.SecretarioDescripcion))
            .ForMember(d => d.FechaAuto, o => o.MapFrom(s => s.FechaAuto))
            .ForMember(d => d.Plantilla, o => o.MapFrom(s => s.Plantilla))
            .ForMember(d => d.AsuntoId, o => o.MapFrom(s => s.AsuntoId))
            .ForMember(d => d.AsuntoDocumentoId, o => o.MapFrom(s => s.AsuntoDocumentoId))
            .ForMember(d => d.NombreArchivo, o => o.MapFrom(s => s.NombreArchivo))
            .ForMember(d => d.NombreCapDJ, o => o.MapFrom(s => s.NombreCapDJ))
            .ForMember(d => d.EstadoAutorizacion, o => o.MapFrom(s => s.EstadoAutorizacion))
            .ForMember(d => d.NumeroAlias, o => o.MapFrom(s => s.NumeroAlias))
            .ForMember(d => d.ArchivoPromocion, o => o.MapFrom(s => s.ArchivoPromocion))
            .ForMember(d => d.NombreOrigen, o => o.MapFrom(s => s.NombreOrigen))
            .ForMember(d => d.Origen, o => o.MapFrom(s => s.OrigenId))
            .ForMember(d => d.UserNameOficial, o => o.MapFrom(s => s.UserNameOficial))
            .ForMember(d => d.EmpleadoPreAutoriza, o => o.MapFrom(s => s.EmpleadoPreAutoriza))
            .ForMember(d => d.EmpleadoCancela, o => o.MapFrom(s => s.EmpleadoCancela))
            .ForMember(d => d.EmpleadoElimina, o => o.MapFrom(s => s.EmpleadoElimina))
            .ForMember(d => d.FechaPreAutoriza, o => o.MapFrom(s => s.FechaPreAutoriza))
            .ForMember(d => d.FechaAutoriza, o => o.MapFrom(s => s.FechaAutoriza))
            .ForMember(d => d.FechaCancela, o => o.MapFrom(s => s.FechaCancela))
            .ForMember(d => d.FechaElimina, o => o.MapFrom(s => s.FechaElimina))
            .ForMember(d => d.UserNameCapDJ, o => o.MapFrom(s => s.UserNameCapDJ))
            .ForMember(d => d.UserNameSecretario, o => o.MapFrom(s => s.UserNameSecretario))
            .ForMember(d => d.FechaRecibido_F, o => o.MapFrom(s => s.FechaRecibido_F))
            .ForMember(d => d.FechaAuto_F, o => o.MapFrom(s => s.FechaAuto_F))
            .ForMember(d => d.NombreDocumento, o => o.MapFrom(s => s.NombreDocumento))
            .ForMember(d => d.YearPromocion, o => o.MapFrom(s => s.YearPromocion))
            .ForMember(d => d.TipoCuadernoId, o => o.MapFrom(s => s.TipoCuadernoId))
            .ForMember(d => d.NombreCorto, o => o.MapFrom(s => s.NombreCorto))
            .ForMember(d => d.RutaArchivoNAS, o => o.MapFrom(s => s.RutaArchivoNAS))
            .ForMember(d => d.RutaArchivoNAS, o => o.MapFrom(s => s.RutaArchivoNAS))
            .ForMember(d => d.EsPromocionE, o => o.MapFrom(s => s.EsPromocionE))
            .ForMember(d => d.OficiosFirmados, o => o.MapFrom(s => s.OficiosFirmados))
            .ForMember(d => d.CanceloCuenta, o => o.MapFrom(s => s.CanceloCuenta))
            .ForMember(d => d.Promovente, o => o.MapFrom(s => s.Promovente));
        CreateMap<ContadoresEstadosTramiteTablero, MetaDataEstadosTramite>()
            .ForMember(d => d.TotalSinAcuerdo, o => o.MapFrom(s => s.SinAcuerdo))
            .ForMember(d => d.TotalCancelados, o => o.MapFrom(s => s.Cancelados))
            .ForMember(d => d.TotalConAcuerdo, o => o.MapFrom(s => s.ConAcuerdo))
            .ForMember(d => d.TotalPreAutorizados, o => o.MapFrom(s => s.PreAutorizados))
            .ForMember(d => d.TotalAutorizados, o => o.MapFrom(s => s.Autorizados))
            .ForMember(d => d.TotalTramites, o => o.MapFrom(s => s.Total));
    }
    private void MapSubirAcuerdo()
    {
        CreateMap<SubirAcuerdoComando, AgregarDocumento>()
        .ForMember(d => d.FechaAcuerdo, o => o.MapFrom(s => MappingUtils.ObtenerFechaDeCadena(s.FechaAcuerdo)));
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
