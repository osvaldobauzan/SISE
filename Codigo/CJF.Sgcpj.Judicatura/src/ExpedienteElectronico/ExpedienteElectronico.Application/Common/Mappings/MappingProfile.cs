using System.Reflection;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Common.Application.Utils;
using ExpedienteElectronico.Application.Common.Models;
using ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.DatosGenerales;

namespace CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Mappings;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        MapDatosGenerales();
    }

    private void MapDatosGenerales()
    {
        CreateMap<DatosGeneralesDto, DatosGeneralesM>()
            .ForMember(d => d.FechaOCC, o => o.MapFrom(s => MappingUtils.ObtenerFechaDeCadena(s.FechaOCC)))
            .ForMember(d => d.FechaOrg, o => o.MapFrom(s => MappingUtils.ObtenerFechaDeCadena(s.FechaOrg)));
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
