using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;
/// <summary>
///                         DECLARACION ENTIDADES PARA OFICIO UNICAMENTE PARA SEGUIMIENTO
/// </summary>
public class Oficio : Documentos
{

    public int Anio { get; set; }

    public int CatOrganismoId { get; set; }

    public int TipoAnexo { get; set; }

    public string NombreParte { get; set; }

    public int Folio { get; set; }

}
