using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;
public class Paging
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public long TotalRows { get; set; }

    public int PageCount
    {
        get
        {
            double pages = 0;
            if (TotalRows > 0)
            {
                pages = (double)TotalRows / PageSize;
                pages = TotalRows % PageSize == 0 ? pages : pages + 1;
            }
            return (int)pages;
        }
    }

    public bool All
    {
        get
        {
            if (TotalRows <= PageSize)
            {
                return true;
            }
            return false;
        }
    }

    public Paging()
    {
        PageNumber = 1;
        PageSize = 200;
        TotalRows = 0;
    }
}