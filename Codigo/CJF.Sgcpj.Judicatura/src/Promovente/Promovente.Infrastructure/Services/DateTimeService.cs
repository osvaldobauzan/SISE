﻿using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;

namespace CJF.Sgcpj.Judicatura.Promovente.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
