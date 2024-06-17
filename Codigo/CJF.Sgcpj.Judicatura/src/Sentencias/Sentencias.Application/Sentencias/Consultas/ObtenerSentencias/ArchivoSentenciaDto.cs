using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Consultas.ObtenerSentencias;

public  class ArchivoSentenciaDto
{
    public long AsuntoNeunId { get; set; }

    public long AsuntoDocumentoId { get; set; }

    public long SintesisOrden { get; set; }

    public long CatOrganismoId { get; set; }

    public string NombreArchivo { get; set; }

    public string ExtensionDocumento { get; set; }

    public string NomArchivoReal { get; set; }

    public string Sentencia { get; set; }

    public DateTime FechaAlta { get; set; }

    public string UserNameTitular { get; set; }

    public string UserNameSecretario { get; set; }
}
