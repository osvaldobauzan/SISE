using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
public class SubirAcuseM
{

    public long AsuntoNeunId { get; set; }
    public int SintesisOrden { get; set; }
    public int CatOrganismoId { get; set; }
    public DateTime FechaNotificacion { get; set; }
    public int TipoAcuse { get; set; }
    public long PersonaId { get; set; }
    public long EmpleadoId { get; set; }
    public string SintesisCitatorio { get; set; }
    public int? TipoNotificacion { get; set; }
    public DateTime? FechaNotificacionCitatorio { get; set; }

}

public class SubirAcuseArchivoM
{

    public long NotElecId { get; set; }
    public string NombreArchivo { get; set; }
    public string ExtensionDocumento { get; set; }
    public long Usuario { get; set; }
    public int Origen { get; set; }
    public int TipoAcuse { get; set; }
    public long IdRuta { get; set; }


}
