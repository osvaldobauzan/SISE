using MediatR;

namespace CJF.Sgcpj.Judicatura.Usuarios.Application.Usuarios.Secretarios;

public record class ObtieneCatalogoSecretario : IRequest<List<CatalogoSecretarioDto>>
{
    public int CatOrganismoId { get; set; }
}

