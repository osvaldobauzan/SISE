using CJF.Sgcpj.Judicatura.Sentencias.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Consultas.ObtenerSentencias;
using CJF.Sgcpj.Judicatura.Sentencias.Domain.Entities;

namespace CJF.Sgcpj.Judicatura.Sentencias.Application.Common.Repositories;

public interface ISentenciasRepository
{
    Task<List<SentenciaDto>> ObtenerSentencias(ConsultaSentencias filtroSentencias);

    Task<RegistroSentencia> GuardarSentencia(Sentencia sentencia);

    Task<bool> GuardarSentenciaVersionPublica(SentenciaVP sentenciaVP);

    Task<string> GuardarBitacora(RegistroBitacora bitacora);

    Task<string> GuardarDeterminacionJudicial(RegistroDeterminacionJudicial determinacion);

    Task<bool> GuardarRelacionSentenciaSISE3(SentenciaSISE3 relacionarSentencia);

    Task ActualizaUsuarioAsuntosDocumentos(UsuarioAsuntosDocumentos parametro);

    Task ActualizaAutorizacionDocumentos(AutorizacionDocumentos parametro);
}
