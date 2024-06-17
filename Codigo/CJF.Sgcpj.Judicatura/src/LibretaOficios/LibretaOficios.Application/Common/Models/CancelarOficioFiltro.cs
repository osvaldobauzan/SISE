using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CJF.Sgcpj.Judicatura.LibretaOficios.Application.Common.Models;
public record CancelarOficioFiltro : IRequest<bool>
{

    /// <summary>
    /// Obtiene el id del organo jurisdiccional
    /// </summary>
    public int CatOrganismoId { get; set; }
    /// <summary>
    /// Número de folio ca cancelar
    /// </summary>
    public int Folio { get; set; }
    /// <summary>
    /// Año del folio a cancelar
    /// </summary>
    public int Anio { get; set; }
    /// <summary>
    /// Empleado que realizó la cancelación
    /// </summary>
    public long EmpleadoId { get; set; }
}