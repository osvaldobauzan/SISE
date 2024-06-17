using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
public class RutasNas
{
    public int KId { get; set; }
    public int Igrupo { get; set; }
    public string Sdescripcion { get; set; }
    public int ItipoArchivo { get; set; }
    public string StipoArchivoDesc { get; set; }
    public string Sruta { get; set; }
    public bool Iescritura { get; set; }
}
