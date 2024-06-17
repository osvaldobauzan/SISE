using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace CJF.Sgcpj.Judicatura.LibretaOficios.Application.Common.Models;
public record LibretaOficioFiltro : IRequest<List<LibretaOficio>>
{
    /// <summary>
    /// Obtiene o establece el identificador del NEUN.
    /// </summary>
    public long AsuntoNeunId { get; set; }
    /// <summary>
    /// Obtiene el id del organo jurisdiccional
    /// </summary>
    public int CatOrganismoId { get; set; }
    /// <summary>
    /// Feche de inicio para el filtrado de la consulta por fecha de alta 
    /// </summary>
    public DateTime? FechaInicio { get; set; }
    /// <summary>
    /// Feche de fin para el filtrado de la consulta por fecha de alta 
    /// </summary>
    public DateTime? FechaFin { get; set; }
    /// <summary>
    /// Establece el folio a buscar
    /// </summary>
    public int Folio { get; set; }
    /// <summary>
    /// Establece el año a buscar
    /// </summary>
    public int Anio { get; set; }
    /// <summary>
    /// Establece los números de registros a mostrar
    /// </summary>
    public int NoRegistros { get; set; }
    /// <summary>
    /// Establece la cantidad de registros a mostrar
    /// </summary>
    public int CantidadRegistros { get; set; }
    /// <summary>
    /// Establece el empleado que realiza la consulta
    /// </summary>
    public long EmpleadoId { get; set; }
}
