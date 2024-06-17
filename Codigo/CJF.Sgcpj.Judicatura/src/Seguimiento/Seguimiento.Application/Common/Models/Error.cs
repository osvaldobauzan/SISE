using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;
public class Error
{
    private Exception _internalException;
    private string _internalExceptionDescription;

    public int ErrorCode { get; set; }

    public string Description { get; set; }

    public string InternalExceptionDescription
    {
        get { return _internalExceptionDescription; }

    }

    public Exception InternalException
    {
        get
        {
            if (_internalException == null)
            {
                _internalException = new Exception();
            }
            return _internalException;
        }
        set
        {
            _internalException = value;
            _internalExceptionDescription = value.ToString();
        }
    }

    public Error() { }
    public Error(string ErrorMsg)
    {
        Description = ErrorMsg;
    }
}