using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Usuarios.Application.Usuarios.BusquedaParteExistente.Consulta;
public class BusquedaParteFiltro : IRequest<List<BusquedaParteDTO>>
{
    public string? CatOrganismoId { get; set; }
    public int? CatTipoPersonaId { get; set; }
    public string? Nombre { get; set; }
    public string? APaterno { get; set; }
    public string? AMaterno { get; set; }
    public DateTime? FechaInicial { get; set; }
    public DateTime? FechaFinal { get; set; }
    public int? Anio { get; set; }
    public int? CatTipoAsuntoId { get; set; }
    public int? OrganismoIdConsulta { get; set; }
    public int? UsuarioId { get; set; }
}
