using System.Globalization;
using System.Reflection;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Usuarios.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Usuarios.Application.Usuarios.AutoridadJudicial.Consulta.AutoridadJudicialExistente;

namespace CJF.Sgcpj.Judicatura.Usuarios.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        MapAutoridadJudicialPorAsunto();
    }

    private void MapAutoridadJudicialPorAsunto()
    {
        CreateMap<Autoridad, ObtieneAutoridadJudicialExistenteDto>()
        .ForMember(d => d.NombreCompleto, o => o.MapFrom(s => s.Nombres))
        .ForMember(d => d.EmpleadoId, o => o.MapFrom(s => s.AutoridadJudicialId))
        .ForMember(d => d.CargoDescripcion, o => o.MapFrom(s => s.AutoridadJudicialDescripcion))
        .ForMember(d => d.CatOrganismoId, o => o.MapFrom(s => s.CatOrganismoId))
        .ForMember(d => d.NombreOficial, o => o.MapFrom(s => s.NombreOficial));
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
