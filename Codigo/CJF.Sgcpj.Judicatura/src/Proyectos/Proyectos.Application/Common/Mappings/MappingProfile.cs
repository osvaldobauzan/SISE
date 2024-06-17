using System.Reflection;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Common.Application.Utils;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Comandos.ActualizarEstadoProyectoComando;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Comandos.SubirProyectoConAudienciaComando;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerProyectos;
using CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerVersionesProyecto;
using CJF.Sgcpj.Judicatura.Proyectos.Domain.Entities;
using Proyectos.Application.Common.Models;
using Proyectos.Domain.Entities;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        MapTableroProyectos();
        MapProyectoArchivo();
        MapListadoVeriones();
        MapActualizarEstadoProyecto();
        MapUltimaVersionProyecto();
        MapObtenerMotivosPartes();
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

    private void MapTableroProyectos()
    {
        CreateMap<ObtenerProyectosConFiltro, ConsultaPaginadaProyectos>()
           .ForMember(d => d.FechaInicial, o => o.MapFrom(s => MappingUtils.ObtenerFechaDeCadena(s.FechaInicial)))
          .ForMember(d => d.FechaFinal, o => o.MapFrom(s => MappingUtils.ObtenerFechaDeCadena(s.FechaFinal)));

        CreateMap<ProyectosItemTablero, Expediente>()
            .ForMember(d => d.AsuntoNeunId, o => o.MapFrom(s => s.AsuntoNeunId))
            .ForMember(d => d.AsuntoAlias, o => o.MapFrom(s => s.AsuntoAlias))
            .ForMember(d => d.CatTipoOrganismoId, o => o.MapFrom(s => s.CatTipoOrganismoId))
            .ForMember(d => d.CatOrganismoId, o => o.MapFrom(s => s.CatOrganismoId))
            .ForMember(d => d.CatTipoAsunto, o => o.MapFrom(s => s.CatTipoAsunto))
            .ForMember(d => d.CatTipoAsuntoId, o => o.MapFrom(s => s.CatTipoAsuntoId))
            .ForMember(d => d.NombreCorto, o => o.MapFrom(s => s.NombreCorto))
            .ForMember(d => d.TipoProcedimiento, o => o.MapFrom(s => s.TipoProcedimiento))
            .ForMember(d => d.TipoCuadernoId, o => o.MapFrom(s => s.TipoCuaderno))
            .ForMember(d => d.Cuaderno, o => o.MapFrom(s => s.sTipoCuaderno));

        CreateMap<ProyectosItemTablero, Proyecto>()
            .ForPath(d => d.Expediente.AsuntoNeunId, o => o.MapFrom(s => s.AsuntoNeunId))
            .ForPath(d => d.Expediente.AsuntoAlias, o => o.MapFrom(s => s.AsuntoAlias))
            .ForPath(d => d.Expediente.CatTipoOrganismoId, o => o.MapFrom(s => s.CatTipoOrganismoId))
            .ForPath(d => d.Expediente.CatOrganismoId, o => o.MapFrom(s => s.CatOrganismoId))
            .ForPath(d => d.Expediente.CatTipoAsunto, o => o.MapFrom(s => s.CatTipoAsunto))
            .ForPath(d => d.Expediente.CatTipoAsuntoId, o => o.MapFrom(s => s.CatTipoAsuntoId))
            .ForPath(d => d.Expediente.NombreCorto, o => o.MapFrom(s => s.NombreCorto))
            .ForPath(d => d.Expediente.TipoProcedimiento, o => o.MapFrom(s => s.TipoProcedimiento))
            .ForPath(d => d.Expediente.TipoCuadernoId, o => o.MapFrom(s => s.TipoCuaderno))
            .ForPath(d => d.Expediente.Cuaderno, o => o.MapFrom(s => s.sTipoCuaderno))
            .ForMember(d => d.TieneAudiencia, o => o.MapFrom(s => s.TieneAudiencia))
            .ForMember(d => d.FechaAudiencia, o => o.MapFrom(s => s.FechaAudiencia))
            .ForMember(d => d.TieneArchivoAudiencia, o => o.MapFrom(s => s.TieneArchivoAudiencia))
            .ForMember(d => d.ArchivoAudiencia, o => o.MapFrom(s => s.ArchivoAudiencia))
            .ForMember(d => d.Secretario, o => o.MapFrom(s => s.Secretario))
            .ForMember(d => d.Mesa, o => o.MapFrom(s => s.Mesa))
            .ForMember(d => d.TieneArchivoProyecto, o => o.MapFrom(s => s.TieneArchivoProyecto))
            .ForMember(d => d.ArchivoProyecto, o => o.MapFrom(s => s.ArchivoProyecto))
            .ForMember(d => d.FechaCargaProyecto, o => o.MapFrom(s => s.FechaCargaProyecto))
            .ForMember(d => d.NumeroVersionProyecto, o => o.MapFrom(s => s.NumeroVersionProyecto))
            .ForMember(d => d.EstadoProyecto, o => o.MapFrom(s => s.EstadoProyecto))
            .ForMember(d => d.DescripcionEstadoProyecto, o => o.MapFrom(s => s.sEstadoProyecto))
            .ForMember(d => d.FechaEstadoProyecto, o => o.MapFrom(s => s.FechaEstadoProyecto))
            .ForMember(d => d.SentidoProyecto, o => o.MapFrom(s => s.SentidoProyecto))
            .ForMember(d => d.DescripcionSentidoProyecto, o => o.MapFrom(s => s.sSentido))
            .ForMember(d => d.TipoSentencia, o => o.MapFrom(s => s.TipoSentencia))
            .ForMember(d => d.DescripcionTipoSentencia, o => o.MapFrom(s => s.sTipoSentencia))
            .ForMember(d => d.ProyectoId, o => o.MapFrom(s => s.pkProyectoId))
            .ForMember(d => d.AsuntoDocumentoId, o => o.MapFrom(s => s.AsuntoDocumentoId));

        CreateMap<ContadoresEstadosProyectosTablero, MetaDataEstadoProyecto>()
            .ForMember(d => d.TotalSinProyecto, o => o.MapFrom(s => s.TotalSinProyecto))
            .ForMember(d => d.TotalParaRevision, o => o.MapFrom(s => s.TotalParaRevision))
            .ForMember(d => d.TotalConAjustes, o => o.MapFrom(s => s.TotalConAjustes))
            .ForMember(d => d.TotalNoAprobado, o => o.MapFrom(s => s.TotalNoAprobado))
            .ForMember(d => d.TotalAprobado, o => o.MapFrom(s => s.TotalAprobado))
            .ForMember(d => d.TotalProyectos, o => o.MapFrom(s => s.TotalProyectos));
    }

    private void MapProyectoArchivo()
    {
        CreateMap<ProyectoConAudiencia, ProyectoConAudienciaDto>()
            .ForMember(d => d.ProyectoId, o => o.MapFrom(s => s.pkProyectoId))
            .ForMember(d => d.CatOrganismoId, o => o.MapFrom(s => s.CatOrganismoId))
            .ForMember(d => d.AsuntoNeunId, o => o.MapFrom(s => s.AsuntoNeunId))
            .ForMember(d => d.FechaCreacion, o => o.MapFrom(s => s.fFechaProyecto))
            .ForMember(d => d.TitularId, o => o.MapFrom(s => s.iTitular))
            .ForMember(d => d.SecretarioId, o => o.MapFrom(s => s.iSecretario))
            .ForMember(d => d.TipoSentenciaId, o => o.MapFrom(s => s.iTipoSentenciaId))
            .ForMember(d => d.SentidoId, o => o.MapFrom(s => s.iSentidoId))
            .ForMember(d => d.ProyectoArchivoId, o => o.MapFrom(s => s.fkProyectoVersionArchivoId))
            .ForMember(d => d.NombreArchivo, o => o.MapFrom(s => s.sNombreArchivo))
            .ForMember(d => d.Sintesis, o => o.MapFrom(s => s.sSintesis))
            .ForMember(d => d.Version, o => o.MapFrom(s => s.iVersion))
            .ForMember(d => d.EstadoId, o => o.MapFrom(s => s.iEstado))
            .ForMember(d => d.Anio, o => o.MapFrom(s => s.sAnioRuta))
            .ForMember(d => d.Secretario, o => o.MapFrom(s => s.UserNameSecretario))
            .ForMember(d => d.Titular, o => o.MapFrom(s => s.UserNameTitular))
            .ForMember(d => d.NumeroExpediente, o => o.MapFrom(s => s.AsuntoAlias))
            .ForMember(d => d.TipoAsunto, o => o.MapFrom(s => s.CatTipoAsunto));
    }

    private void MapListadoVeriones()
    {
        CreateMap<ListadoVersiones, ListadoVersionesDto>()
            .ForMember(d => d.AsuntoNeunId, o => o.MapFrom(s => s.AsuntoNeunId))
            .ForMember(d => d.NumeroExpediente, o => o.MapFrom(s => s.AsuntoAlias))
            .ForMember(d => d.TipoAsuntoId, o => o.MapFrom(s => s.TipoAsuntoId))
            .ForMember(d => d.TipoAsunto, o => o.MapFrom(s => s.TipoAsunto))
            .ForMember(d => d.CuadernoId, o => o.MapFrom(s => s.CuadernoId))
            .ForMember(d => d.Cuaderno, o => o.MapFrom(s => s.Cuaderno))
            .ForMember(d => d.CatOrganismoId, o => o.MapFrom(s => s.CatOrganismoId))
            .ForMember(d => d.CatOrganismo, o => o.MapFrom(s => s.CatOrganismo));

        CreateMap<ListadoVersiones, VersionDto>()
            .ForPath(d => d.ProyectoId, o => o.MapFrom(s => s.pkProyectoId))
            .ForPath(d => d.ArchivoId, o => o.MapFrom(s => s.pkProyectoArchivoId))
            .ForPath(d => d.NombreArchivo, o => o.MapFrom(s => s.sNombreArchivo))
            .ForPath(d => d.Anio, o => o.MapFrom(s => s.sAnioRuta))
            .ForPath(d => d.NumeroVersion, o => o.MapFrom(s => s.iVersion))
            .ForPath(d => d.EstadoId, o => o.MapFrom(s => s.iEstado))
            .ForPath(d => d.EstadoDescripcion, o => o.MapFrom(s => s.EstadoProyecto))
            .ForPath(d => d.TitularId, o => o.MapFrom(s => s.iTitular))
            .ForPath(d => d.NombreTitular, o => o.MapFrom(s => s.NombreTitular))
            .ForPath(d => d.SecretarioId, o => o.MapFrom(s => s.iSecretario))
            .ForPath(d => d.NombreSecretario, o => o.MapFrom(s => s.NombreSecretario))
            .ForPath(d => d.TipoSentenciaId, o => o.MapFrom(s => s.iTipoSentenciaId))
            .ForPath(d => d.TipoSentenciaDescripcion, o => o.MapFrom(s => s.TipoSentencia))
            .ForPath(d => d.TipoSentidoId, o => o.MapFrom(s => s.iSentidoId))
            .ForPath(d => d.TipoSentidoDescripcion, o => o.MapFrom(s => s.SentidoSentencia))
            .ForPath(d => d.Sintesis, o => o.MapFrom(s => s.ComentarioSecretario))
            .ForPath(d => d.ComentarioTitular, o => o.MapFrom(s => s.ComentarioTitular))
            .ForPath(d => d.ArchivoComentarioId, o => o.MapFrom(s => s.fkCorreccionArchivoId))
            .ForPath(d => d.NombreArchivoObservaciones, o => o.MapFrom(s => s.CorreccionArchivo))
            .ForPath(d => d.FechaAlta, o => o.MapFrom(s => s.fFechaAltaArchivoProyecto));
    }

    private void MapActualizarEstadoProyecto()
    {
        CreateMap<ProyectoActualizado, EstadoProyectoActualizadoDto>()
            .ForMember(d => d.AsuntoNeunId, o => o.MapFrom(s => s.AsuntoNeunId))
            .ForMember(d => d.NumeroExpediente, o => o.MapFrom(s => s.AsuntoAlias))
            .ForMember(d => d.TipoAsuntoId, o => o.MapFrom(s => s.CatTipoAsuntoId))
            .ForMember(d => d.TipoAsunto, o => o.MapFrom(s => s.CatTipoAsunto))
            .ForMember(d => d.CuadernoId, o => o.MapFrom(s => s.TipoCuaderno))
            .ForMember(d => d.Cuaderno, o => o.MapFrom(s => s.sTipoCuaderno))
            .ForMember(d => d.CatOrganismoId, o => o.MapFrom(s => s.CatOrganismoId))
            .ForMember(d => d.CatOrganismo, o => o.MapFrom(s => s.CatOrganismo))
            .ForMember(d => d.ProyectoId, o => o.MapFrom(s => s.pkProyectoId))
            .ForMember(d => d.EstadoId, o => o.MapFrom(s => s.iEstado))
            .ForMember(d => d.NombreArchivo, o => o.MapFrom(s => s.sNombreArchivo))
            .ForMember(d => d.Anio, o => o.MapFrom(s => s.sAnioRuta))
            .ForMember(d => d.ProyectoArchivoId, o => o.MapFrom(s => s.pkProyectoArchivoId))
            .ForMember(d => d.Titular, o => o.MapFrom(s => s.UserNameTitular))
            .ForMember(d => d.Secretario, o => o.MapFrom(s => s.UserNameSecretario))
            .ForMember(d => d.TitularId, o => o.MapFrom(s => s.iTitular))
            .ForMember(d => d.SecretarioId, o => o.MapFrom(s => s.iSecretario))
            .ForMember(d => d.Version, o => o.MapFrom(s => s.iVersion))
            .ForMember(d => d.FechaAlta, o => o.MapFrom(s => s.fFechaAlta));
    }

    private void MapUltimaVersionProyecto()
    {
        CreateMap<ListadoVersiones, VersionDto>()
            .ForPath(d => d.ProyectoId, o => o.MapFrom(s => s.pkProyectoId))
            .ForPath(d => d.ArchivoId, o => o.MapFrom(s => s.pkProyectoArchivoId))
            .ForPath(d => d.NombreArchivo, o => o.MapFrom(s => s.sNombreArchivo))
            .ForPath(d => d.Anio, o => o.MapFrom(s => s.sAnioRuta))
            .ForPath(d => d.NumeroVersion, o => o.MapFrom(s => s.iVersion))
            .ForPath(d => d.EstadoId, o => o.MapFrom(s => s.iEstado))
            .ForPath(d => d.EstadoDescripcion, o => o.MapFrom(s => s.EstadoProyecto))
            .ForPath(d => d.TitularId, o => o.MapFrom(s => s.iTitular))
            .ForPath(d => d.NombreTitular, o => o.MapFrom(s => s.NombreTitular))
            .ForPath(d => d.SecretarioId, o => o.MapFrom(s => s.iSecretario))
            .ForPath(d => d.NombreSecretario, o => o.MapFrom(s => s.NombreSecretario))
            .ForPath(d => d.TipoSentenciaId, o => o.MapFrom(s => s.iTipoSentenciaId))
            .ForPath(d => d.TipoSentenciaDescripcion, o => o.MapFrom(s => s.TipoSentencia))
            .ForPath(d => d.TipoSentidoId, o => o.MapFrom(s => s.iSentidoId))
            .ForPath(d => d.TipoSentidoDescripcion, o => o.MapFrom(s => s.SentidoSentencia))
            .ForPath(d => d.Sintesis, o => o.MapFrom(s => s.ComentarioSecretario))
            .ForPath(d => d.ComentarioTitular, o => o.MapFrom(s => s.ComentarioTitular))
            .ForPath(d => d.ArchivoComentarioId, o => o.MapFrom(s => s.fkCorreccionArchivoId))
            .ForPath(d => d.NombreArchivoObservaciones, o => o.MapFrom(s => s.CorreccionArchivo))
            .ForPath(d => d.FechaAlta, o => o.MapFrom(s => s.fFechaAltaArchivoProyecto));

    }


    private void MapObtenerMotivosPartes()
    {
        CreateMap<MotivosParte, Proyectos.Consultas.ObtenerMotivosPartes.MotivosParteDto>()
            .ForMember(d => d.IdParte, o => o.MapFrom(s => s.IdParte))
            .ForMember(d => d.Parte, o => o.MapFrom(s => s.Parte))
            .ForMember(d => d.IdMotivo, o => o.MapFrom(s => s.IdMotivo))
            .ForMember(d => d.Motivo, o => o.MapFrom(s => s.Motivo))
            .ForMember(d => d.IdSentido, o => o.MapFrom(s => s.IdSentido))
            .ForMember(d => d.Sentido, o => o.MapFrom(s => s.sSentido))
            .ForMember(d => d.Descripcion, o => o.MapFrom(s => s.Descripcion));
    }
}
