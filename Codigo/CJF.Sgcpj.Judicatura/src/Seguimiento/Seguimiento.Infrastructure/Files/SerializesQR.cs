using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Infrastructure.Files;
public  class SerializesQR 
{
    public  static string Serializes(String QrString)
    {
        string QR;
        string[] QrS;

        QR = QrString.Replace("{\"E\"", "{Seguimiento");
        QR = QR.Replace("{\"", "{");
        QR = QR.Replace("N\":", "AsuntoNeun:\"");
        QR = QR.Replace("},\"P\":{", ",");
        QR = QR.Replace("},\"O\":{", ",");
        QR = QR.Replace("},\"A\":{", ",");
        QR = QR.Replace("},\"E\":{", ",");
        QR = QR.Replace("NP\":", "DocumentoId:");
        QR = QR.Replace(",DocumentoId:", "\",DocumentoId:");
        QrS = QR.Split(',');
        if (QrS[1].EndsWith("}"))
        {
            QR = QrS[0] + "," + QrS[1];
        }
        else
        {
            QR = QrS[0] + "," + QrS[1] + "}}";
        }

        return QR;
    }

   
}
