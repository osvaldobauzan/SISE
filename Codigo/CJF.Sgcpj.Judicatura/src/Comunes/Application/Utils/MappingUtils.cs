using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Common.Application.Utils;
public static class MappingUtils
{
    public static DateTime ObtenerFechaDeCadena(string fecha)
    {
        return DateTime.ParseExact(fecha, "dd/MM/yyyy",
                                        CultureInfo.InvariantCulture);
    }

    public static string ObtenerCadenaDeFecha(DateTime? fecha, string formato)
    {
        DateTime fechaValida = fecha ?? DateTime.MinValue;

        return fechaValida.ToString(formato);
    }
}
