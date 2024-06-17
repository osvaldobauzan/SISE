using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;

namespace CJF.Sgcpj.Judicatura.Seguridad.Application.Common.Helpers;
public class PrivilegiosSISE3
{
    private IPrivilegiosService _privilegiosService;

    private  List<Privilegio> _privilegios;


    public List<Privilegio> Privilegios { get { return _privilegios; } }

    public PrivilegiosSISE3(IPrivilegiosService privilegiosService)
    {
        _privilegiosService = privilegiosService;


        InicializarAsync().ConfigureAwait(false).GetAwaiter().GetResult();
    }

    public async Task InicializarAsync()
    {
        if (_privilegios == null)
        {
            _privilegios = await _privilegiosService.ObtenerConfiguracionPrivilegios();
        }
    }

}
