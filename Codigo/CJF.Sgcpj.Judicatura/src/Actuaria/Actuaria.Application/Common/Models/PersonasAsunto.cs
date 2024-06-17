using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
public class PersonasAsunto
{
    public int AsuntoId { get; set; }
    public long AsuntoNeunId { get; set; }
    public string Nombre { get; set; }
    public string? APaterno { get; set; }
    public string? AMaterno { get; set; }
    public int Sexo { get; set; }
    public int MayorEdad { get; set; }
    public Int16 CatTipoPersonaJuridicaId { get; set; }
    public int CatAutoridadId { get; set; }
    public string DescripcionCaracterPersona { get; set; }
    public string DescripcionTipoAsunto { get; set; }
    public long UsuarioCaptura { get; set; }
    public int AceptaOponePublicarDatos { get; set; }
}
