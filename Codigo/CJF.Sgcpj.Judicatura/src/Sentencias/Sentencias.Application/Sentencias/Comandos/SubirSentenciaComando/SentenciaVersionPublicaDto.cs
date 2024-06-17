using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Sentencias.Domain.Entities;

namespace CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Comandos.SubirSentenciaComando;

public class SentenciaVersionPublicaDto : IMapFrom<SentenciaVP>
{
    public long AsuntoNeunId { get; set; }

    public int NumeroOrden { get; set; }

    public int SintesisOrden { get; set; }

    public int TipoOrigen { get; set; }

    public bool DelincuenciaOrganizada { get; set; }

    public bool Confidencial { get; set; }

    public string FraccionConfidencial { get; set; }

    public string MotivacionConfidencial { get; set; }

    public string ObservacionesConfidencial { get; set; }

    public long UsuarioCaptura { get; set; }

    public bool Reservada { get; set; }

    public string FraccionReservada { get; set; }

    public string MotivacionReservada { get; set; }

    public string ObservacionesReservada { get; set; }
}
