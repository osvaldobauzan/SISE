﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Infrastructure.Persistence;
public static class HelperExtentions
{
    public static string RemoveDiacritics(this string s)
    {
        string normalizedString = s.Normalize(NormalizationForm.FormD);
        StringBuilder stringBuilder = new StringBuilder();

        for (int i = 0; i < normalizedString.Length; i++)
        {
            char c = normalizedString[i];
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                stringBuilder.Append(c);
        }

        return stringBuilder.ToString();
    }

    public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty, bool desc)
    {
        string command = desc ? "OrderByDescending" : "OrderBy";
        var type = typeof(TEntity);
        var property = type.GetProperty(orderByProperty);
        var parameter = Expression.Parameter(type, "p");
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var orderByExpression = Expression.Lambda(propertyAccess, parameter);
        var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
            source.Expression, Expression.Quote(orderByExpression));
        return (IOrderedQueryable<TEntity>)source.Provider.CreateQuery<TEntity>(resultExpression);
    }
}
