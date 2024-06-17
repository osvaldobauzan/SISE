using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Exceptions;
public class RuleException : Exception
{
    public string Mensaje { get; set; }
    private readonly string splitter = "Mensaje:";

    public RuleException(string mensaje) : base("Ocurrió un error con una regla en la bd")
    {

        string[] splitted = mensaje.Split(new[] { splitter }, StringSplitOptions.None)
                     .Select(value => value.Trim())
                     .ToArray();
        if (splitted.Length == 2)
        {
            this.Mensaje = splitted[1];
        }
        else if (splitted.Length > 2)
        {
            var indexOfErrorMsj = splitted[1].IndexOf("Error");
            if (indexOfErrorMsj < 0)
            {
                indexOfErrorMsj = splitted[1].Length - 1;
            }
            this.Mensaje = splitted[1].Substring(0, indexOfErrorMsj);
        }
        else
        {
            this.Mensaje = mensaje;
        }
    }
}
