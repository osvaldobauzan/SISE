﻿using System.Security.Claims;

namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;

public interface ICurrentUserService
{
    string EmpleadoId { get;  }
    string Name { get;  }

    Guid? Nonce { get; }
    string ApiUrl { get; }
    string Method { get; }
    ClaimsPrincipal Principal { get;  }
}
