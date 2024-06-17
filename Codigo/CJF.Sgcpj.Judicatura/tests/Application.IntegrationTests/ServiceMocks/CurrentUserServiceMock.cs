using System.Security.Claims;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Models;
public class CurrentUserServiceMock : ICurrentUserService
{
    public string EmpleadoId { get; set; } = "6712";

    public string Name { get; set; } = "User test";

    public Guid? Nonce { get; set; } = Guid.NewGuid();

    public string ApiUrl { get; set; } = "/api/expediente/numero";

    public string Method { get; set; } = "GET";

    public ClaimsPrincipal Principal { get; set; } = new ClaimsPrincipal(new List<ClaimsIdentity>()
    {
        new ClaimsIdentity(new List<Claim>()
        {
            new Claim("empleadoId", "value")
        })
    });
}
