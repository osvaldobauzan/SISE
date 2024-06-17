using CJF.Sgcpj.Judicatura.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Application.Usuarios.PromoventeExistente.Consulta;
public class CatalogoPromoventeExistenteDto : IMapFrom<CatalogoPromoventeExistente>
{
    public int PersonaId { get; set; }
    public long AsuntoNeunId { get; set; }
    public int AsuntoId { get; set; }
    public int PersonaIdReal { get; set; }
    public int Tipo { get; set; }
    public string Nombre { get; set; }
    public string AMaterno { get; set; }
    public string APaterno { get; set; }
    public int Sexo { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public int TipoIdentificador { get; set; }
    public string Email { get; set; }
    public int Uso { get; set; }
    public bool Estatus { get; set; }
    public int RegistroEmpleadoId { get; set; }
    public DateTime FechaCaptura { get; set; }
    public string? CalleNumero { get; set; }
    public int CodigoPostal { get; set; }
    public string Colonia { get; set; }
    public int PromoventeId { get; set; }
    public int EsInterconexion { get; set; }
}
