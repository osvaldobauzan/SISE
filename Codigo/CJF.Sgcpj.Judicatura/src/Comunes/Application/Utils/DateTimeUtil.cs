using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Common.Application.Utils;
public static class DateTimeUtil
{
    public static DateTime ObtenerHoraLocal() {
       return TimeZoneInfo.ConvertTime(DateTime.Now,
                               TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)"));
    }
}
