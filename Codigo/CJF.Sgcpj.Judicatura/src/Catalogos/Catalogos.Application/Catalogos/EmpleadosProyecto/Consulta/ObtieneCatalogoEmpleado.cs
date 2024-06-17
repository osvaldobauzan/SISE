using MediatR;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.EmpleadosProyecto.Consulta;

public record ObtieneCatalogoEmpleado : IRequest<List<CatalogoEmpleadoDto>>
{
    public int CatOrganismoId { get; set; }

    public int TipoEmpleado { get; set; }
}
