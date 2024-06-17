using MediatR;

namespace ExpedienteElectronico.Application.Common.Models;
public class PersonaAsuntoUpdate : IRequest<bool>
{
    public Int64 PersonaId { get; set; }
    public Int64 AsuntoNeunId { get; set; }
    public Int64 UsuarioCaptura { get; set; }
    public int IdOrganoPlenos { get; set; }
    public PersonaAsuntoDTO PersonaAsunto { get; set; }
    public string PersonaAsuntoJson
    {
        get
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(PersonaAsunto);
        }
    }
}
