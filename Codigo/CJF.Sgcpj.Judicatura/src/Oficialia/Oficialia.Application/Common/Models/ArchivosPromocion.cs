using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Application.Common.Models;
public class ArchivosPromocion
{
    public ArchivosPromocion()
    {
        Archivos = new List<Documento>();
        Anexos = new List<Documento>();
        Electronicos = new List<Documento>();
    }
    public List<Documento> Archivos { get; set; }
    public List<Documento> Anexos { get; set; }
    public List<Documento> Electronicos { get; set; }
}

public class Documento
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string? guidDocumento { get; set; }
}
