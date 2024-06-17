using System.Globalization;
using System.Reflection;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Application.Promociones.Comandos;
using CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.InsertarPromocion;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtenerPromocionDetalle;
using CJF.Sgcpj.Judicatura.Application.Promociones.Consulta.ObtienePromocionesConFiltro;
using CJF.Sgcpj.Judicatura.Common.Application.Utils;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Comandos.AgregarPromociones;
using CJF.Sgcpj.Judicatura.Oficialia.Domain.Entities;
using InsertarPromocion = CJF.Sgcpj.Judicatura.Application.Common.Models.InsertarPromocion;

namespace CJF.Sgcpj.Judicatura.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        CreateMap<ObtienePromocionesConFiltroConsulta, ConsultaPaginada>()
            .ForMember(d => d.FechaInicial, o => o.MapFrom(s => MappingUtils.ObtenerFechaDeCadena(s.FechaInicial)))
           .ForMember(d => d.FechaFinal, o => o.MapFrom(s => MappingUtils.ObtenerFechaDeCadena(s.FechaFinal)));
        CreateMap<InsertarPromocionDto, InsertarPromocion>()
            .ForMember(d => d.FechaPresentacion, o => o.MapFrom(s => MappingUtils.ObtenerFechaDeCadena(s.FechaPresentacion)));
        CreateMap<AgregarPromocionDto, AgregarPromocion>()
            .ForMember(d => d.FechaPresentacion, o => o.MapFrom(s => MappingUtils.ObtenerFechaDeCadena(s.FechaPresentacion)));
        CreateMap<EditarPromocionDto, EditarPromocion>()
        .ForMember(d => d.FechaPresentacion, o => o.MapFrom(s => MappingUtils.ObtenerFechaDeCadena(s.FechaPresentacion)))
        .ForMember(d => d.NumeroRegistro, o => o.MapFrom(s => s.Registro));


        MapTablero();
        MapDetalleTablero();

    }

    private void MapTablero()
    {
        CreateMap<PromocionItemTablero, Expediente>()
            .ForMember(d => d.AsuntoNeunId, o => o.MapFrom(s => s.AsuntoNeunId))
            .ForMember(d => d.AsuntoAlias, o => o.MapFrom(s => s.Expediente))
            .ForMember(d => d.CatTipoAsunto, o => o.MapFrom(s => s.CatTipoAsunto))
            .ForMember(d => d.CatTipoAsuntoId, o => o.MapFrom(s => s.CatTipoAsuntoId))
            .ForMember(d => d.NombreCorto, o => o.MapFrom(s => s.Cuaderno))
            .ForMember(d => d.CatOrganismoId, o => o.MapFrom(s => s.CatOrganismoId))
            .ForMember(d => d.AsuntoNeunId, o => o.MapFrom(s => s.AsuntoNeunId));
        CreateMap<PromocionItemTablero, Promocion>()
            .ForPath(d => d.Expediente.AsuntoNeunId, o => o.MapFrom(s => s.AsuntoNeunId))
            .ForPath(d => d.Expediente.AsuntoAlias, o => o.MapFrom(s => s.Expediente))
            .ForPath(d => d.Expediente.CatTipoAsunto, o => o.MapFrom(s => s.CatTipoAsunto))
            .ForPath(d => d.Expediente.CatTipoAsuntoId, o => o.MapFrom(s => s.CatTipoAsuntoId))
            .ForPath(d => d.Expediente.NombreCorto, o => o.MapFrom(s => s.Cuaderno))
            .ForPath(d => d.Expediente.CatOrganismoId, o => o.MapFrom(s => s.CatOrganismoId))
            .ForPath(d => d.Expediente.TipoProcedimiento, o => o.MapFrom(s => s.TipoProcedimiento))
            .ForMember(d => d.CuadernoNombre, o => o.MapFrom(s => s.Cuaderno))
            .ForMember(d => d.CuadernoId, o => o.MapFrom(s => s.CuadernoId))
            .ForMember(d => d.SecretarioId, o => o.MapFrom(s => s.IdSecretario))
            .ForMember(d => d.NumeroRegistro, o => o.MapFrom(s => s.NumeroRegistro))
            .ForMember(d => d.OrigenPromocion, o => o.MapFrom(s => s.OrigenPromocionId))
            .ForMember(d => d.Origen, o => o.MapFrom(s => s.Origen))
            .ForMember(d => d.NombreOrigen, o => o.MapFrom(s => s.NombreOrigen))
            .ForMember(d => d.OrigenPromocionDescripcion, o => o.MapFrom(s => s.OrigenPromocion))
            .ForMember(d => d.SecretarioDescripcion, o => o.MapFrom(s => s.Secretario))
            .ForMember(d => d.SecretarioUserName, o => o.MapFrom(s => s.SecretarioUserName))
            .ForMember(d => d.Mesa, o => o.MapFrom(s => s.Mesa))
            .ForMember(d => d.FechaPresentacion, o => o.MapFrom(s => s.FechaPresentacion))
            .ForMember(d => d.TipoContenidoDescripcion, o => o.MapFrom(s => s.TipoContenido))
            .ForMember(d => d.ParteDescripcion, o => o.MapFrom(s => s.Promovente))
            .ForMember(d => d.ClasePromoventeId, o => o.MapFrom(s => s.IdPromovente))
            .ForMember(d => d.ClasePromoventeDescripcion, o => o.MapFrom(s => s.ClasePromovente))
            .ForMember(d => d.Copias, o => o.MapFrom(s => s.NumeroCopias))
            .ForMember(d => d.Anexos, o => o.MapFrom(s => s.NumeroAnexos))
            .ForMember(d => d.Estado, o => o.MapFrom(s => s.EstatusPromocion))
            .ForMember(d => d.Fojas, o => o.MapFrom(s => s.Fojas))
            .ForMember(d => d.NumeroOrden, o => o.MapFrom(s => s.NumeroOrden))
            .ForMember(d => d.UsuarioCaptura, o => o.MapFrom(s => s.UsuarioCaptura))
            .ForMember(d => d.kIdElectronica, o => o.MapFrom(s => s.kIdElectronica))
            .ForMember(d => d.YearPromocion, o => o.MapFrom(s => s.YearPromocion))
            .ForMember(d => d.ConArchivo, o => o.MapFrom(s => s.ConArchivo))
            .ForMember(d => d.FechaCaptura, o => o.MapFrom(s => s.FechaCaptura))
            .ForMember(d => d.CatAutorizacionDocumentosId, o => o.MapFrom(s => s.CatAutorizacionDocumentosId))
            .ForMember(d => d.OrigenPromocionDesc, o => o.MapFrom(s => s.OrigenPromocionDescripcion))
            .ForMember(d => d.EstadoAcuerdo, o => o.MapFrom(s => s.EstadoAcuerdo))
            .ForMember(d => d.NombreOficial, o => o.MapFrom(s => s.NombreOficial))
            .ForMember(d => d.YearPromocion, o => o.MapFrom(s => s.YearPromocion));
        CreateMap<ContadoresEstadosTablero, MetaDataEstados>()
            .ForMember(d => d.TotalSinCaptura, o => o.MapFrom(s => s.SinCaptura))
            .ForMember(d => d.TotalCapturadas, o => o.MapFrom(s => s.Capturadas))
            .ForMember(d => d.EnviadasAMesa, o => o.MapFrom(s => s.Asignadas))
            .ForMember(d => d.TotalPromociones, o => o.MapFrom(s => s.Total));
    }

    private void MapDetalleTablero()
    {

        CreateMap<PromocionDetalleTablero, ItemDetallePromocionTablero>()
             .ForMember(d => d.PromoventeApellidoMaterno, o => o.MapFrom(s => s.PromoventeApellidoMaterno))
            .ForMember(d => d.PromoventeApellidoPaterno, o => o.MapFrom(s => s.PromoventeApellidoPaterno))
            .ForMember(d => d.ParteAsociadaApellidoPaterno, o => o.MapFrom(s => s.ParteAsociadaApellidoPaterno))
            .ForMember(d => d.ParteAsociadaApellidoMaterno, o => o.MapFrom(s => s.ParteAsociadaApellidoMaterno))
            .ForMember(d => d.Mesa, o => o.MapFrom(s => s.Mesa));

    CreateMap<ItemDetallePromocionTablero, PromocionDetalleTablero>()
             .ForMember(d => d.PromoventeApellidoMaterno, o => o.MapFrom(s => s.PromoventeApellidoMaterno))
            .ForMember(d => d.PromoventeApellidoPaterno, o => o.MapFrom(s => s.PromoventeApellidoPaterno))
            .ForMember(d => d.ParteAsociadaApellidoPaterno, o => o.MapFrom(s => s.ParteAsociadaApellidoPaterno))
            .ForMember(d => d.ParteAsociadaApellidoMaterno, o => o.MapFrom(s => s.ParteAsociadaApellidoMaterno))
            .ForMember(d => d.Folio, o => o.MapFrom(s => s.FolioString))
            .ForMember(d => d.Mesa, o => o.MapFrom(s => s.Mesa));

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
