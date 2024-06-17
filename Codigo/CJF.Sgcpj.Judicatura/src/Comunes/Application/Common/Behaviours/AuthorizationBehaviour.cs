using System;
using System.Reflection;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Exceptions;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Seguridad.Application.Common.Helpers;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private const string NamespaceSeguridad = "Application.Seguridad";
    private readonly ICurrentUserService _currentUserService;
    private readonly ISesionService _sesionService;
    private readonly IConfiguration _configuration;
    private readonly PrivilegiosSISE3 _privilegiosSISE3;

    public AuthorizationBehaviour(ICurrentUserService currentUserService,
                                  ISesionService sesionService,
                                  IConfiguration configuration,
                                  PrivilegiosSISE3 privilegiosSISE3)
    {
        _currentUserService = currentUserService;
        _sesionService = sesionService;
        _configuration = configuration;
        _privilegiosSISE3 = privilegiosSISE3;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
       
            //Atributo para aplicar el behavior al ensamblado
            var assAttr = Assembly.GetAssembly(request.GetType()).GetCustomAttributes<AuthorizeAttribute>();
            //Atributo para ignorar privilegios 
            var noRequierePrivilegios = request.GetType().GetCustomAttributes<SesionNoRequiredAttribute>();

            if (assAttr.Any() && !noRequierePrivilegios.Any())
        {
            //Funciones excuidas de pedir sesion
            bool includeAuth = IsExcludeFunctions(request);

            //Excepción por no usuario válido
            if (includeAuth && _currentUserService != null && _currentUserService.EmpleadoId == null)
            {
                throw new ForbiddenAccessException();
            }
            //Excepción por no tener sesion
            if (includeAuth && _sesionService.SesionActual == null)
            {
                throw new ForbiddenAccessException(); 
            }

            var authorized = false;

            if (!noRequierePrivilegios.Any())//Si la execución requiere privilegios
            {
                if (_sesionService.SesionActual.Privilegios != null)
                {
                    //Se completa la lista de privilegios del usuarios con url y verbo
                    List<Privilegio> usuarioPrivilegiosCompleta = _privilegiosSISE3.Privilegios
                                                      .Where(p => _sesionService.SesionActual.Privilegios.Contains(p.IdPrivilegio)).ToList();
                    //Se hace el cruce entre los privilegios del usuario y la petición
                    authorized = usuarioPrivilegiosCompleta
                        .Exists(p => p.Api.ToLower() == _currentUserService.ApiUrl.ToLower()
                        && p.Verbo.ToLower() == _currentUserService.Method.ToLower());
                }
                if (!authorized)
                {
                    throw new ForbiddenAccessException(true);
                }
            }
        }

        return await next();
    }

    private bool IsExcludeFunctions(TRequest request)
    {
        var excludedFuncsSetting = _configuration["SISE3:BackEnd:AuthNFuncionesExcluidas"];
        var excludedFuncsArray = excludedFuncsSetting is null ? new string[0] : excludedFuncsSetting.Split('|');

        var excludedFuncs = new List<string>();
        if (excludedFuncsArray != null && excludedFuncsArray.Length > 0)
        {
            excludedFuncs.AddRange(excludedFuncsArray);
        }

        excludedFuncs.Add(NamespaceSeguridad);
        var reqAssembly = request.GetType().ToString();
        var includeAuth = !excludedFuncs.Any(excludedFunc => reqAssembly.Contains(excludedFunc));
        return includeAuth;
    }
}
