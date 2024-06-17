using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Common.Application.Utils;
public static class ValidationsUtils
{

    public static bool EsUnaFechaValida(string value)
    {
        DateTime dt;
        return DateTime.TryParseExact(value,
                            "dd/MM/yyyy",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out dt);
    }
}
