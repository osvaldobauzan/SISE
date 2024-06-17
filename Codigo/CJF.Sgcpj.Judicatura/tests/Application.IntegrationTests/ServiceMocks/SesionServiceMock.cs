using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.ServiceMocks;
public class SesionServiceMock : ISesionService
{
    public Sesion SesionActual => new Sesion()
    {
        CargoDescripcion = "Cargo Test",
        CargoId = 1,
        CatOrganismoId = 180,
        CatTipoOrganismoId = 1,
        Completo = "Completo Test",
        ConnectionId = "SignalRTest Connection",
        EMail = "email@test.com",
        EmpleadoId = 6712,
        ExpiracionUtc = DateTime.UtcNow.AddHours(1),
        NombreOficial = "NombreOficial Test",
        Nonce = Guid.NewGuid().ToString(),
        RefrehToken = Guid.NewGuid().ToString(),
        Privilegios = new List<int>() { 1, 2, 3 }
    };
}
