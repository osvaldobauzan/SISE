using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;
public class ArchivosPromocion
{
    public ArchivosPromocion()
    {
        Archivos = new List<Documento>();
        Anexos = new List<Documento>();
    }
    public List<Documento> Archivos { get; set; }
    public List<Documento> Anexos { get; set; }
}

public class Documento
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public Guid GuidDocumento { get; set; }
}
