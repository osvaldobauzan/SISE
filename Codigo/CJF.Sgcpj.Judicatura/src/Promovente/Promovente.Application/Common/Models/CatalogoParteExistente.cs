using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Promovente.Application.Common.Models;

public class CatalogoParteExistente
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
    public int Foraneo{ get; set; }
    public int Tipo { get; set; }
    public int? notiElect { get; set; }
    public string? usuarioRegistro { get; set; }
    public string PersonaTipo { get; set; }
}

public class CatalogoParteAutoridad
{
    public string CargoDescripcion { get; set; }
    public string NombreCompleto { get; set; }
    public string NombreOficial { get; set; }
    public int CatOrganismoId { get; set; }
    public int EmpleadoId { get; set; }
}