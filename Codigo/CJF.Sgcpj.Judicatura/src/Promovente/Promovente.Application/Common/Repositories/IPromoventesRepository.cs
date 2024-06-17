using CJF.Sgcpj.Judicatura.Promovente.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Promovente.Application.Common.Repositories;

public interface IPromoventesRepository
{
    Task<long> AgregarPromovente(AgregarPromovente promovente);
    Task<long> AgregarPersona(AgregarPersonaAsunto personaAsunto);
    Task<long> AgregarAutoridadJudicial(AgregarAutoridadJudicial autoridadjudicioal);
}
