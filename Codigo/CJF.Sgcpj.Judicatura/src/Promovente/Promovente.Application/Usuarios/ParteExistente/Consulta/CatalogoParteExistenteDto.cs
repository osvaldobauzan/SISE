using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Promovente.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Promovente.Application.Usuarios.ParteExistente.Consulta;

public class CatalogoParteExistenteDto : IMapFrom<CatalogoParteExistente>
{
    public string DescripcionTipoPersona { get; set; }
    public string DenominacionDeAutoridad { get; set; }
    public string DescripcionCaracterPersona { get; set; }
    public string DescripcionClasificaAutoridadGenerica { get; set; }
    public long PersonaId { get; set; }
    public string Nombre { get; set; }
    public string AMaterno { get; set; }
    public string APaterno { get; set; }
    public int CatCaracterPersonaAsuntoId { get; set; }
    public int Foraneo { get; set; }
    public int Tipo { get; set; }
    public int? notiElect { get; set; }
    public string? usuarioRegistro { get; set; }
    public string PersonaTipo { get; set; }
}
public class CatalogoParteAutoridadDto : IMapFrom<CatalogoParteAutoridad>
{
    public string CargoDescripcion { get; set; }
    public string NombreCompleto { get; set; }
    public string NombreOficial { get; set; }
    public int CatOrganismoId { get; set; }
    public int EmpleadoId { get; set; }
}