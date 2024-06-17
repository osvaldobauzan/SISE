using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documentos.Application.Common.Model.GenerarEvidenciaFirma;
public class Signature
{
    public string SignerName { get; set; }

    public string SignerCurp { get; set; }

    public string SerialNumber { get; set; }

    public string Pkcs1 { get; set; }

    public string DateSigning { get; set; }

    public string Algorithm { get; set; }

    public byte Sequence { get; set; }

    public int DocumentId { get; set; }

    public string DigestOid { get; set; }
}
