using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documentos.Application.Common.Model.GenerarEvidenciaFirma;
public class DatosFirmantesDto
{
    public string Nombre { get; set; }
    public string NoSerie { get; set; }
    public string Algoritmo { get; set; }
    public string CadenaFirma { get; set; }
    public string FechaFirma { get; set; }
    public string FechaOCSP { get; set; }
    public string EmisorOCSP { get; set; }
    public string FechaTSP { get; set; }
    public string EmisorTSP { get; set; }
    public string EmisorCertificadoTPS { get; set; }
    public string ArchivoFirmado { get; set; }
    public string AutoridadCertificadora { get; set; }
    public string NumeroFirmantes { get; set; }
    public string Estampilla { get; set; }
    public string RespuestaTPS { get; set; }
    public string RespondedorOCSP { get; set; }
    public string SerieOCSP { get; set; }

}