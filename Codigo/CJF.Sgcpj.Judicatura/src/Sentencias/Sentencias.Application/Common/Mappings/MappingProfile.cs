using System.Reflection;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Common.Application.Utils;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Consultas.ObtenerPromociones;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Consultas.ObtenerSentencias;
using CJF.Sgcpj.Judicatura.Sentencias.Domain.Entities;
using SentenciaDto = CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Consultas.ObtenerSentencias.SentenciaDto;

namespace CJF.Sgcpj.Judicatura.Sentencias.Application.Common.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        MapTableroSentencias();
        MapSentencia();
        MapObtenerPromocionesSentencia();
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

    private void MapTableroSentencias()
    {
        CreateMap<ObtenerSentenciasFiltro, ConsultaSentencias>()
            .ForMember(d => d.Fecha, o => o.MapFrom(s => MappingUtils.ObtenerFechaDeCadena(s.Fecha)))
            .ForMember(d => d.FechaFin, o => o.MapFrom(s => MappingUtils.ObtenerFechaDeCadena(s.FechaFin)));

        CreateMap<SentenciaItemTablero, SentenciaDto>()
            .ForPath(d => d.Expediente.AsuntoNeunId, o => o.MapFrom(s => s.AsuntoNeunId))
            .ForPath(d => d.Expediente.AsuntoAlias, o => o.MapFrom(s => s.AsuntoAlias))
            .ForPath(d => d.Expediente.CatOrganismoId, o => o.MapFrom(s => s.CatOrganismoId))
            .ForPath(d => d.Expediente.CatTipoAsunto, o => o.MapFrom(s => s.CatTipoAsunto))
            .ForPath(d => d.Expediente.CatTipoAsuntoId, o => o.MapFrom(s => s.CatTipoAsuntoId))
            .ForPath(d => d.Expediente.CatOrganismo, o => o.MapFrom(s => s.NombreOrganismo))
            .ForPath(d => d.Expediente.TipoCuadernoId, o => o.MapFrom(s => s.TipoCuadernoId))
            .ForPath(d => d.Expediente.Cuaderno, o => o.MapFrom(s => s.TipoCuaderno))
            .ForPath(d => d.Expediente.AsuntoDocumentoId, o => o.MapFrom(s => s.AsuntoDocumentoId))
            .ForPath(d => d.Expediente.GuidDocumento, o => o.MapFrom(s => s.GuidDocumento))
            .ForMember(d => d.SentenciaId, o => o.MapFrom(s => s.SentenciaId))
            .ForMember(d => d.FechaAprobacionProyecto, o => o.MapFrom(s => s.fFechaAprobacionProyecto))
            .ForMember(d => d.TemaDelAsunto, o => o.MapFrom(s => s.TemaAsunto))
            .ForMember(d => d.ArchivoSentenciaId, o => o.MapFrom(s => s.ArchivoSentenciaId))
            .ForMember(d => d.NombreArchivoSentencia, o => o.MapFrom(s => s.Sentencia))
            .ForMember(d => d.ArchivoAcuseId, o => o.MapFrom(s => s.ArchivoAcuseId))
            .ForMember(d => d.NombreArchivoAcuse, o => o.MapFrom(s => s.Acuse))
            .ForMember(d => d.UsuarioCapturo, o => o.MapFrom(s => s.NombreCapturo))
            .ForMember(d => d.FechaCapturo, o => o.MapFrom(s => s.FechaCapturo))
            .ForMember(d => d.UsuarioPreautorizo, o => o.MapFrom(s => s.NombrePreautorizo))
            .ForMember(d => d.FechaPreautorizo, o => o.MapFrom(s => s.FechaPreautorizo))
            .ForMember(d => d.UsuarioAutorizo, o => o.MapFrom(s => s.NombreAutorizo))
            .ForMember(d => d.FechaAutorizo, o => o.MapFrom(s => s.FechaAutorizo))
            .ForMember(d => d.FechaPublicacion, o => o.MapFrom(s => s.FechaPublicacion))
            .ForMember(d => d.FechaAuto, o => o.MapFrom(s => s.FechaAuto))
            .ForMember(d => d.EstadoSentenciaId, o => o.MapFrom(s => s.EstadoSentenciaId))
            .ForMember(d => d.EstadoSentencia, o => o.MapFrom(s => s.EstadoSentencia))
            .ForMember(d => d.TipoOrganismoId, o => o.MapFrom(s => s.CatTipoOrganismoId));
    }

    private void MapSentencia()
    {
        //CreateMap<Sentencia, Sentencias.Comandos.SubirSentenciaComando.SentenciaDto>();
        //CreateMap<SentenciaVP, SentenciaVersionPublicaDto>();
    }

    private void MapObtenerPromocionesSentencia()
    {
        CreateMap<Promocion, PromocionSentenciaDto>()
            .ForMember(d => d.NumeroRegistro, o => o.MapFrom(s => s.NumeroRegistro))
            .ForMember(d => d.FechaPresentacion, o => o.MapFrom(s => s.FechaPresentacion))
            .ForMember(d => d.Promovente, o => o.MapFrom(s => s.Promovente))
            .ForMember(d => d.Contenido, o => o.MapFrom(s => s.Contenido))
            .ForMember(d => d.NumeroOrden, o => o.MapFrom(s => s.NumeroOrden))
            .ForMember(d => d.ClasePromovente, o => o.MapFrom(s => s.ClasePromovente))
            .ForMember(d => d.TipoPromovente, o => o.MapFrom(s => s.TipoPromovente))
            .ForMember(d => d.SintesisOrden, o => o.MapFrom(s => s.SintesisOrden))
            .ForMember(d => d.FechaAcuerdo, o => o.MapFrom(s => s.FechaAcuerdo))
            .ForMember(d => d.PromocionSeleccionada, o => o.MapFrom(s => s.PromocionSeleccionada))
            .ForMember(d => d.EstadoPromocion, o => o.MapFrom(s => s.EstadoPromocion))
            .ForMember(d => d.YearPromocion, o => o.MapFrom(s => s.YearPromocion))
            .ForMember(d => d.TipoCuaderno, o => o.MapFrom(s => s.TipoCuaderno))
            ;
    }
}
