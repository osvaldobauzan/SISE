using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Models;
public class ConsultaPaginadaDetalle
{
    public int CatOrganismoId { get; set; }
    public int TamanioPagina { get; set; }
    public int NumeroPagina { get; set; }
    public long AsuntoNeunId { get; set; }
    public long AsuntoDocumentoID { get; set; }
    public string Texto { get; set; }
    public string OrdenarPor { get; set; }
    public bool TipoOrden { get; set; }
    public int? FiltroTipo { get; set; }
    public int? FiltroTipoParteID { get; set; }
    public int? FiltroTipoNotificacionID { get; set; }
    public long? FiltroActuarioID { get; set; }
    public bool PrimeraCarga { get; set; }
}
