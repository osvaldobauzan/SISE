using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Seguimientos.InsertarSeguimiento.Comando;
using FluentValidation;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;


namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Seguimientos.InsertarSeguimiento.Comando;
public class InsertarSeguimientoConFiltroValidator : AbstractValidator<Common.Models.Seguimiento>
{
    public InsertarSeguimientoConFiltroValidator()
    {
        RuleFor(c => c.QrString)
         .NotEmpty().WithMessage("El QR es requerido")
         .Must(f => IsJsonDocument(f))
         .WithMessage("El QR es inválido");

        RuleFor(c => c.EmpleadoId)
         .NotEmpty().WithMessage("El EmpleadoId es requerido");

        RuleFor(c => c.CatOrganismoId)
         .NotEmpty().WithMessage("El CatOrganismoId es requerido");

        RuleFor(c => c.DocumentoId)
          .NotEmpty().WithMessage("El DocumentoId es requerido");

    }
    public bool IsJsonDocument(string QrString)
    {
        try
        {
            var obj = JsonDocument.Parse(QrString);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
}
